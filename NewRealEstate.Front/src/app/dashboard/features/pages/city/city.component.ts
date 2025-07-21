import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { environment } from '../../../../../environments/environment.development';
import { City } from '../../../../core/models/city';
import { CityService } from '../../../../core/services/city.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { CommonModule } from '@angular/common';
import { ModalComponent } from "../../../../shared/components/modal/modal.component";
import { UploaderComponent } from "../../../../shared/components/uploader/uploader.component";

@Component({
  selector: 'app-city',
  imports: [CommonModule, ModalComponent, ReactiveFormsModule, UploaderComponent],
  templateUrl: './city.component.html',
  styleUrl: './city.component.css'
})
export class CityComponent implements OnInit {
  private readonly service = inject(CityService);
  private readonly toastr = inject(NotificationService)
  private readonly builder = inject(FormBuilder);
  cities: City[] = [];

  isModalOpen = false;
  form!: FormGroup;
  formMode: 'create' | 'edit' = 'create';
  editingCityId: number | null = null;
  notifications: { type: string, message: string }[] = [];


  ngOnInit(): void {
    this.loadCities();


    this.form = this.builder.group({
      name: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  loadCities() {
    this.service.get().subscribe((cities) => {
      this.cities = cities;
    });
  }

  getImageUrl(path: City): string {
    return path.image ? `${environment.assetsUrl}/${path.image}` : '';
  }

  openModal() {
    this.formMode = 'create';
    this.editingCityId = null;
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
          this.loadCities();
          this.closeModal();
          this.toastr.success('City created successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    } else if (this.formMode === 'edit' && this.editingCityId) {
      formData.append('id', this.editingCityId.toString());
      this.service.update(this.editingCityId, formData).subscribe({
        next: () => {
          this.loadCities();
          this.closeModal();
          this.toastr.success('City updated successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    }
  }


  editCity(City: City) {
    this.formMode = 'edit';
    this.editingCityId = City.id;
    this.isModalOpen = true;
    this.form.patchValue({
      name: City.name,
    });
  }

  changeStatus(City: City) {
    this.service.updateStatus(City.id).subscribe({
      next: () => {
        this.loadCities();
        this.toastr.success('City status updated successfully!');
      },
      error: () => this.toastr.error('Data not saved'),
    });
  }

  deleteCity(id: number) {
    if (!confirm('Are you want to delete City?')) return;
    this.service.delete(id).subscribe(() => {
      this.toastr.success('City deleted successfully!');
      this.loadCities();
    });
  }
}
