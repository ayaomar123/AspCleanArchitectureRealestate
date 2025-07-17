import { Component, inject, OnInit, } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { environment } from '../../../../../environments/environment.development';
import { Category } from '../../../../core/models/category';
import { CategoryService } from '../../../../core/services/category.service';
import { FormGroup } from '@angular/forms';
import { NotificationService } from '../../../../shared/services/notification.service';
import { CommonModule } from '@angular/common';
import { ModalComponent } from "../../../../shared/components/modal/modal.component";
import { UploaderComponent } from "../../../../shared/components/uploader/uploader.component";

@Component({
  selector: 'app-category-page',
  imports: [ReactiveFormsModule, CommonModule, ModalComponent, UploaderComponent],
  templateUrl: './category-page.component.html',
  styleUrl: './category-page.component.css'
})
export class CategoryPageComponent implements OnInit {
  private readonly service = inject(CategoryService);
  private readonly toastr = inject(NotificationService)
  private readonly builder = inject(FormBuilder);
  categories: Category[] = [];

  isModalOpen = false;
  form!: FormGroup;
  formMode: 'create' | 'edit' = 'create';
  editingCategoryId: number | null = null;
  notifications: { type: string, message: string }[] = [];


  ngOnInit(): void {
    this.loadCategories();


    this.form = this.builder.group({
      name: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  loadCategories() {
    this.service.get().subscribe((categories) => {
      this.categories = categories;
    });
  }

  getImageUrl(path: Category): string {
    return path.image ? `${environment.assetsUrl}/${path.image}` : '';
  }

  openModal() {
    this.formMode = 'create';
    this.editingCategoryId = null;
    this.form.reset();
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
    formData.append('name', data.name);
    if (data.image) {
      formData.append('image', data.image);
    }

    if (this.formMode === 'create') {
      this.service.create(formData).subscribe({
        next: () => {
          this.loadCategories();
          this.closeModal();
          this.toastr.success('Category created successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    } else if (this.formMode === 'edit' && this.editingCategoryId) {
      formData.append('id', this.editingCategoryId.toString());
      this.service.update(this.editingCategoryId, formData).subscribe({
        next: () => {
          this.loadCategories();
          this.closeModal();
          this.toastr.success('Category updated successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    }
  }


  editCategory(category: Category) {
    this.formMode = 'edit';
    this.editingCategoryId = category.id;
    this.isModalOpen = true;
    this.form.patchValue({
      name: category.name,
    });
  }

  changeStatus(category: Category) {
    this.service.updateStatus(category.id).subscribe({
      next: () => {
        this.loadCategories();
        this.toastr.success('Category status updated successfully!');
      },
      error: () => this.toastr.error('Data not saved'),
    });
  }

  deleteCategory(id: number) {
    if (!confirm('Are you want to delete category?')) return;
    this.service.delete(id).subscribe(() => this.loadCategories());
  }
}
