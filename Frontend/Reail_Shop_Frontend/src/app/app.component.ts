import { Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InvoiceComponent } from './Components/component-invoice/invoice.component';
import { ToastsComponent } from './Components/Toast/toasts/toasts.component';
import { ToastService } from './services/toast.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, InvoiceComponent, ToastsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent  {
  title = 'Reail_Shop_Frontend';
  constructor(private toastService: ToastService) {}
  
}
