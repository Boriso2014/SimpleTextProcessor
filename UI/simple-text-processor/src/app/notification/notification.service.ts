import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private _toastrService: ToastrService) { }

  public showSuccess = (message: string, title?: string) => {
    this._toastrService.success(message, title, {
      timeOut: 3000,
      progressBar: true,
      positionClass: 'toast-top-center'
    });
  }

  public showWarning = (message: string, title?: string) => {
    this._toastrService.warning(message, title, {
      timeOut: 8000,
      progressBar: true,
      positionClass: 'toast-top-center'
    });
  }

  public showError = (message: string, title?: string) => {
    this._toastrService.error(message, title, {
      disableTimeOut: true,
      positionClass: 'toast-top-center'
    });
  }
}