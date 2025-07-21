import { Component, inject, OnInit } from '@angular/core';
import { ItemService } from '../../../../core/services/item.service';
import { Image, Item } from '../../../../core/models/item';
import { ActivatedRoute } from '@angular/router';
import { StatusLabelPipe } from "../../../../shared/pipes/status-label.pipe";
import { CommonModule } from '@angular/common';
import { environment } from '../../../../../environments/environment.development';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModalComponent } from "../../../../shared/components/modal/modal.component";
import { UploaderComponent } from "../../../../shared/components/uploader/uploader.component";
import { ItemImageService } from '../../../../core/services/item-image.service';
import { NotificationService } from '../../../../shared/services/notification.service';

@Component({
  selector: 'app-item-details',
  imports: [StatusLabelPipe, CommonModule, ModalComponent, ReactiveFormsModule, UploaderComponent],
  templateUrl: './item-details.component.html',
  styleUrl: './item-details.component.css'
})
export class ItemDetailsComponent implements OnInit {

  private readonly service = inject(ItemService);
  private readonly imgService = inject(ItemImageService);
  private readonly route = inject(ActivatedRoute);
  private readonly builder = inject(FormBuilder);
  private readonly toastr = inject(NotificationService)

  activeTab: string = 'images';
  itemId: number | null = null;
  item: Item | undefined;
  isModalOpen = false;
  form!: FormGroup;

  ngOnInit(): void {
    this.itemId = +this.route.snapshot.paramMap.get('id')!;
    this.loadItem();

    this.form = this.builder.group({
      itemId: [this.itemId],
      image: ['', [Validators.required]],
    });
  }

  private loadItem(): void {
    this.service.getById(this.itemId!).subscribe(item => {
      this.item = item;
    });
  }

  getImageUrl(path: Item): string {
    return path.image ? `${environment.assetsUrl}/${path.image}` : '';
  }

  getItemImageUrl(path: Image): string {
    return path.imageUrl ? `${environment.assetsUrl}/${path.imageUrl}` : '';
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  onImageUploaded(file: File) {
    this.form.patchValue({ image: file });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const data = this.form.value;
    const formData = new FormData();
    formData.append('itemId', data.itemId);
    if (data.image) {
      formData.append('image', data.image);
    }


    this.imgService.create(formData).subscribe({
      next: () => {
        this.loadItem();
        this.closeModal();
        this.toastr.success('Image created successfully!');
      },
      error: () => this.toastr.error('Data not saved')
    });
  }

  changeStatus(img: Image) {
    this.imgService.updateStatus(img.id).subscribe({
      next: () => {
        this.loadItem();
        this.toastr.success('Item status updated successfully!');
      },
      error: (err) => {
        console.log(err)
        this.toastr.error('Data not saved')
      },
    });
  }

  deleteImage(id: number) {
    if (!confirm('Are you sure you want to delete this image?')) return;

    this.imgService.delete(id).subscribe({
      next: () => {
        this.toastr.success('Image deleted successfully!');
        this.loadItem();
      },
      error: (err) => {
        console.log(err)
        this.toastr.error('Failed to delete image.')
      }
    });
  }

}
