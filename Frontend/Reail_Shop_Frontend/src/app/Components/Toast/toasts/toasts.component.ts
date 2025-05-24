// toasts.component.ts
import { Component, OnInit } from '@angular/core';
import { ToastService, ToastMessage } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  imports: [CommonModule], 
  styleUrls: ['./toasts.component.css']
})
export class ToastsComponent implements OnInit {
  message = '';
  type: ToastMessage['type'] = 'success';
  isVisible = false;

  constructor(private toastService: ToastService) {}
  ngOnInit() {
    this.toastService.toastState$.subscribe(({ message, type }) => {
       if (!message) return; 
      this.message = message;
      this.type = type;
      this.isVisible = true;

      setTimeout(() => this.isVisible = false, 3000);
    });
  }
}

