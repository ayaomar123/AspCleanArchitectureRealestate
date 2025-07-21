import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { environment } from '../../../../../environments/environment.development';
import { District } from '../../../../core/models/district';
import { DistrictService } from '../../../../core/services/district.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { CommonModule } from '@angular/common';
import { ModalComponent } from "../../../../shared/components/modal/modal.component";
import { UploaderComponent } from "../../../../shared/components/uploader/uploader.component";
import { CityService } from '../../../../core/services/city.service';
import { City } from '../../../../core/models/city';

@Component({
  selector: 'app-district',
  imports: [ReactiveFormsModule, CommonModule, ModalComponent, UploaderComponent],
  templateUrl: './district.component.html',
  styleUrl: './district.component.css'
})
export class DistrictComponent implements OnInit {
  private readonly service = inject(DistrictService);
  private readonly cityService = inject(CityService);
  private readonly toastr = inject(NotificationService)
  private readonly builder = inject(FormBuilder);
  districts: District[] = [];
  cities: City[] = [];

  isModalOpen = false;
  form!: FormGroup;
  formMode: 'create' | 'edit' = 'create';
  editingDistrictId: number | null = null;
  notifications: { type: string, message: string }[] = [];


  ngOnInit(): void {
    this.loadDistricts();
    this.loadCities();


    this.form = this.builder.group({
      name: ['', [Validators.required]],
      cityId: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  loadDistricts() {
    this.service.get().subscribe((districts) => {
      this.districts = districts;
    });
  }
  loadCities() {
    this.cityService.get().subscribe((cities) => {
      this.cities = cities;
    });
  }

  getImageUrl(path: District): string {
    return path.image ? `${environment.assetsUrl}/${path.image}` : '';
  }

  openModal() {
    this.formMode = 'create';
    this.editingDistrictId = null;
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
    formData.append('cityId', data.cityId);
    if (data.image) {
      formData.append('image', data.image);
    }

    if (this.formMode === 'create') {
      this.service.create(formData).subscribe({
        next: () => {
          this.loadDistricts();
          this.closeModal();
          this.toastr.success('District created successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    } else if (this.formMode === 'edit' && this.editingDistrictId) {
      formData.append('id', this.editingDistrictId.toString());
      this.service.update(this.editingDistrictId, formData).subscribe({
        next: () => {
          this.loadDistricts();
          this.closeModal();
          this.toastr.success('District updated successfully!');
        },
        error: () => this.toastr.error('Data not saved')
      });
    }
  }


  editDistrict(district: District) {
    this.formMode = 'edit';
    this.editingDistrictId = district.id;
    this.isModalOpen = true;
    this.form.patchValue({
      name: district.name,
      cityId: district.cityId
    });
  }

  changeStatus(District: District) {
    this.service.updateStatus(District.id).subscribe({
      next: () => {
        this.loadDistricts();
        this.toastr.success('District status updated successfully!');
      },
      error: () => this.toastr.error('Data not saved'),
    });
  }

  deleteDistrict(id: number) {
    if (!confirm('Are you want to delete District?')) return;
    this.service.delete(id).subscribe(() => {
      this.toastr.success('District deleted successfully!');
      this.loadDistricts();
    });
  }
}
