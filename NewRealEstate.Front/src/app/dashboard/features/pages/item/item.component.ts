import { Component, inject, OnInit } from '@angular/core';
import { ItemService } from '../../../../core/services/item.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { environment } from '../../../../../environments/environment.development';
import { NotificationService } from '../../../../shared/services/notification.service';
import { Item } from '../../../../core/models/item';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { StatusLabelPipe } from "../../../../shared/pipes/status-label.pipe";

@Component({
  selector: 'app-item',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, StatusLabelPipe, RouterLink],
  templateUrl: './item.component.html',
  styleUrl: './item.component.css'
})
export class ItemComponent implements OnInit {

  private readonly service = inject(ItemService);
  private readonly toastr = inject(NotificationService)
  private readonly builder = inject(FormBuilder);
  items: Item[] = [];


  ngOnInit(): void {
    this.loadItems();

  }

  loadItems() {
    this.service.get().subscribe((items) => {
      this.items = items;
    });
  }



  getImageUrl(path: Item): string {
    return path.image ? `${environment.assetsUrl}/${path.image}` : '';
  }

  changeStatus(item: Item) {
    this.service.updateStatus(item.id).subscribe({
      next: () => {
        this.loadItems();
        this.toastr.success('Item status updated successfully!');
      },
      error: () => this.toastr.error('Data not saved'),
    });
  }

  deleteItem(id: number) {
    if (!confirm('Are you want to delete Item?')) return;
    this.service.delete(id).subscribe(() => {
      this.toastr.success('Item deleted successfully!');
      this.loadItems();
    });
  }
}
