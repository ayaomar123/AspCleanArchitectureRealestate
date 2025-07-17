import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../services/notification.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [CommonModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent implements OnDestroy {
  notifications: { type: string; message: string }[] = [];
  private subscription: Subscription;

  constructor(private toastr: NotificationService) {
    this.subscription = this.toastr.notifications$.subscribe(notification => {
      this.notifications.push(notification);
      setTimeout(() => this.notifications.shift(), 3000);
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
