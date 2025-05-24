import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { ToastService } from '../../services/toast.service';

interface Product {
  productId: number;
  productName: string;
  unitPrice: number;
  itemInvoice: any;
}
interface InvoiceResponse {
  message: string;
  invoiceId: number;
  totalAmount: number;
  balanceAmount: number;
  transactionDate: string;}

@Component({
  selector: 'app-component-invoice',
  standalone: true,
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css'],
  imports: [FormsModule, CommonModule],  
  providers: [ApiService]
})
export class InvoiceComponent implements OnInit {
  
  products: Product[] = [];
  InvoiceResponse: InvoiceResponse [] = [];
  invoiceItems: any[] = [];
  items: any[] = [];

  messageColor: 'success' | 'error' = 'success';
  productId: number = 0;  
  selectedProductName: string = '';
  quantity: number = 1;
  discount: number = 0;
  unitPrice: number = 0;
  transactionDate: string = '';
  invoiceId!: number;
  totalAmount!: number;
  balanceAmount!: number;
  showInvoiceSummary: boolean = false;
  showDateWarning = false;

  //current Date
  today = new Date().toISOString().split('T')[0];

  constructor(private apiService: ApiService, private toastService: ToastService) {}
   ngOnInit() {
    this.apiService.getProductDetails().subscribe(data => {
      this.products = data;
    });
  }

  //add item to tempory table
  addItem() {
    if (this.selectedProductName && this.quantity > 0) {
      this.items.push({
        productId:this.productId,
        productName: this.selectedProductName,
        quantity: this.quantity,
        discount: this.discount,
        unitPrice: this.unitPrice,
      });

      // Reset fields
      this.selectedProductName = '';
      this.quantity = 1;
      this.discount = 0;
      this.unitPrice = 0;
    }
  }

  onProductSelect() {
  const selected = this.products.find(p => p.productName === this.selectedProductName);
  this.unitPrice = selected ? selected.unitPrice : 0;
  this.productId = selected ? selected.productId : 0;
}

//remove item from tempory table
  removeItem(index: number) {
    this.items.splice(index, 1);
  }

//hide tempory table
hideTemporyTable(): void {
  this.showInvoiceSummary = false;
}

//create invoice
  submit(invoiceForm: NgForm) {
  this.showDateWarning = false;
    const invoiceData = {
      items: this.items,
      transactionDate: this.transactionDate
    };

    this.apiService.createInvoice(invoiceData).subscribe(
       (response: InvoiceResponse) => {
      this.toastService.show(response.message, 'success');

      this.invoiceId = response.invoiceId;
      this.totalAmount = response.totalAmount;
      this.balanceAmount = response.balanceAmount;
      this.transactionDate = response.transactionDate;
      this.showInvoiceSummary = true;

      this.apiService.getItemInvoice(this.invoiceId).subscribe(
        (itemResponse) => {
          this.invoiceItems = itemResponse;
          this.showInvoiceSummary = true;
        },
        (error) => {
          console.error('Error when fetching items in invoice:', error);
        }
      );
        this.items = [];
    },
      (error) => {
       const errMsg = error.error?.message;
      this.toastService.show(errMsg, 'error');
      }
    );

  }
}
