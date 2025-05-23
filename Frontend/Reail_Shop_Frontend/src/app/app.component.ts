import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InvoiceComponent } from './Components/component-invoice/invoice.component';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, InvoiceComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  template: `<app-component-invoice></app-component-invoice>`,
})
export class AppComponent {
  title = 'Reail_Shop_Frontend';
}
