import { Component, inject, OnInit } from '@angular/core';
import { UploaderComponent } from "../../../../shared/components/uploader/uploader.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { District } from '../../../../core/models/district';
import { ItemService } from '../../../../core/services/item.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../../../core/services/category.service';
import { CityService } from '../../../../core/services/city.service';
import { DistrictService } from '../../../../core/services/district.service';
import { Category } from '../../../../core/models/category';
import { City } from '../../../../core/models/city';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-item-form',
  imports: [UploaderComponent, CommonModule, ReactiveFormsModule],
  templateUrl: './item-form.component.html',
  styleUrl: './item-form.component.css'
})
export class ItemFormComponent implements OnInit {
  form!: FormGroup;
  categories: Category[] = [];
  cities: City[] = [];
  districts: District[] = [];
  selectedImageFile?: File;

  //for editing
  isEditMode = false;
  itemId: number | null = null;

  private readonly categoryService = inject(CategoryService);
  private readonly cityService = inject(CityService);
  private readonly districtService = inject(DistrictService);
  private readonly service = inject(ItemService);
  private readonly toastr = inject(NotificationService);
  private readonly builder = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  fieldConfigs = [
    { name: 'name', label: 'Name', type: 'text' },
    { name: 'advertiseNo', label: 'Advertise No', type: 'text' },
    { name: 'categoryId', label: 'Category', type: 'select', options: this.categories },
    { name: 'cityId', label: 'City', type: 'select', options: this.cities },
    { name: 'districtId', label: 'District', type: 'select', options: this.districts },
    { name: 'propertyTypeId', label: 'Property Type', type: 'select', options: this.categories },
    { name: 'latitude', label: 'Latitude', type: 'number' },
    { name: 'longitude', label: 'Longitude', type: 'number' },
    { name: 'soum', label: 'Soum', type: 'number' },
    { name: 'limit', label: 'Limit', type: 'number' },
    { name: 'streetWidth', label: 'Street Width', type: 'number' },
    { name: 'space', label: 'Space', type: 'number' },
    { name: 'pricePerMeter', label: 'Price per Meter', type: 'number' },
    { name: 'hashedPassword', label: 'Password', type: 'password' }
  ];

  ngOnInit(): void {
    this.itemId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.itemId;
    if (this.isEditMode) {
      this.loadItem();
    }

    this.loadCategories();
    this.loadCities();
    this.loadDistricts();

    this.form = this.builder.group({
      image: ['', Validators.required],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      advertiseNo: ['', Validators.required],
      categoryId: ['', Validators.required],
      cityId: ['1', Validators.required],
      districtId: ['', Validators.required],
      propertyTypeId: ['1', Validators.required],
      latitude: [0, Validators.required],
      longitude: [0, Validators.required],
      soum: [0, Validators.required],
      limit: [0, Validators.required],
      streetWidth: [0, Validators.required],
      space: [0, Validators.required],
      pricePerMeter: [0, Validators.required],
      description: ['', Validators.maxLength(255)],
      hashedPassword: ['']
    });
  }

  private loadItem(): void {
    this.service.getById(this.itemId!).subscribe(item => {
      console.log(item)
      this.form.patchValue(item);
    });
  }

  private loadCategories() {
    this.categoryService.get().subscribe((data) => {
      this.categories = data;
      this.fieldConfigs.find(f => f.name === 'categoryId')!.options = data;
      this.fieldConfigs.find(f => f.name === 'propertyTypeId')!.options = data;
    });
  }

  private loadCities() {
    this.cityService.get().subscribe((data) => {
      this.cities = data;
      this.fieldConfigs.find(f => f.name === 'cityId')!.options = data;
    });
  }

  private loadDistricts() {
    this.districtService.get().subscribe(data => {
      this.districts = data;
      this.fieldConfigs.find(f => f.name === 'districtId')!.options = data;
    });
  }


  onImageSelected(file: File): void {
    this.selectedImageFile = file;
    this.form.get('image')?.setValue(file);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      debugger;
      this.form.markAllAsTouched();
      return;
    }


    const formValue = this.form.value;
    const formData = new FormData();

    Object.keys(formValue).forEach(key => {
      formData.append(key, formValue[key] ?? '');
    });

    if (this.selectedImageFile) {
      formData.append('Image', this.selectedImageFile);
    }

    const request = this.isEditMode
      ? this.service.update(this.itemId!, formData)
      : this.service.create(formData);

    request.subscribe({
      next: () => {
        this.toastr.success(this.isEditMode ? 'Item updated successfully!' : 'Item created successfully!');
        this.router.navigate(['/admin/items']);
      },
      error: () => this.toastr.error('Data not saved')
    });
  }
}

