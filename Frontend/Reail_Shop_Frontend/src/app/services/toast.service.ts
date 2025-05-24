import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export type ToastMessage = {
  message: string;
  type: 'success' | 'error';
};

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private toastSubject = new BehaviorSubject<ToastMessage>({ message: '', type: 'success' });
  toastState$ = this.toastSubject.asObservable();

  show(message: string, type: 'success' | 'error' = 'success') {
    this.toastSubject.next({ message, type });
  }
}
