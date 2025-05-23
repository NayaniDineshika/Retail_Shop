import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

interface Product {
  productId: number;
  productName: string;
  unitPrice: number;
  itemInvoice: any;
}
interface InvoiceResponse {
  invoiceId: number;
  totalAmount: number;
  balanceAmount: number;
  transactionDate: string;}
@Component({
  selector: 'app-component-invoice',
  standalone: true,
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css'],
  imports: [FormsModule, CommonModule],  // Import FormsModule in AppModule or standalone if needed
  providers: [ApiService]
})
export class InvoiceComponent implements OnInit {
  products: Product[] = [];
  InvoiceResponse: InvoiceResponse [] = [];
  invoiceItems: any[] = [];
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

  items: any[] = [];

  today = new Date().toISOString().split('T')[0];

isFutureDate(): boolean {
  return !!this.transactionDate && this.transactionDate > this.today;
}

  constructor(private apiService: ApiService) {}
   ngOnInit() {
    this.apiService.getProductDetails().subscribe(data => {
      this.products = data;
      console.log(this.products);
    });
    
  }
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


  removeItem(index: number) {
    this.items.splice(index, 1);
  }

  submit() {
    console.log('Items');
    const invoiceData = {
      items: this.items,
      transactionDate: this.transactionDate
    };

    this.apiService.createInvoice(invoiceData).subscribe(
       (response: InvoiceResponse) => {
      this.invoiceId = response.invoiceId;
      this.totalAmount = response.totalAmount;
      this.balanceAmount = response.balanceAmount;
      this.transactionDate = response.transactionDate;
      this.showInvoiceSummary = true;

      this.apiService.getItemInvoice(this.invoiceId).subscribe(
        (itemResponse) => {
          this.invoiceItems = itemResponse; // store in a new variable
          this.showInvoiceSummary = true;
        },
        (error) => {
          console.error('Error fetching items in invoice:', error);
        }
      );
    },
      (error) => {
        console.error('Error creating invoice:', error);
        // Handle error response
      }
    );

  }
}
