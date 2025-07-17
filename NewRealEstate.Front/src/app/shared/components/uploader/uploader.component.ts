import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-uploader',
  imports: [CommonModule],
  templateUrl: './uploader.component.html',
  styleUrl: './uploader.component.css'
})
export class UploaderComponent {
  previewUrl: string | null = null;
  uploading = false;

  @Output() imageUploaded = new EventEmitter<File>();

onFileSelected(event: Event): void {
  const file = (event.target as HTMLInputElement)?.files?.[0];
  if (!file) return;

  const reader = new FileReader();
  reader.onload = () => {
    this.previewUrl = reader.result as string;
  };
  reader.readAsDataURL(file);

  this.uploading = true;

  // نمرر الملف مباشرة
  this.imageUploaded.emit(file);
  this.uploading = false;
}

}
