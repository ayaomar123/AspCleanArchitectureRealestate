import { Component } from '@angular/core';
import { SidebarComponent } from "../../components/sidebar/sidebar.component";
import { HeaderComponent } from "../../components/header/header.component";
import { FooterComponent } from "../../components/footer/footer.component";
import { RouterOutlet } from '@angular/router';
import { ToastComponent } from "../../../../shared/components/toast/toast.component";

@Component({
  selector: 'app-admin-page',
  imports: [SidebarComponent, HeaderComponent, FooterComponent, RouterOutlet, ToastComponent],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.css'
})
export class AdminPageComponent {

}
