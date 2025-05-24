import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Product {
  productId: number;
  productName: string;
  unitPrice: number;
  itemInvoice: any;
}
export interface InvoiceResponse {
  message: string;
  invoiceId: number;
  totalAmount: number;
  balanceAmount: number;
  transactionDate: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7278/api/'; 
  constructor(private http: HttpClient) { }

// Get all products
 getProductDetails(): Observable<Product[]> {
  return this.http.get<Product[]>(`${this.baseUrl}Product/getProducts`);
}

//Create invoice
 createInvoice(data: any): Observable<InvoiceResponse> {
  return this.http.post<InvoiceResponse>(`${this.baseUrl}Invoice/createInvoice`, data);
}

//Get invoice details for a specific invoiceId
  getItemInvoice(invoiceId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}Invoice/itemInvoice/${invoiceId}`);
  }
}
