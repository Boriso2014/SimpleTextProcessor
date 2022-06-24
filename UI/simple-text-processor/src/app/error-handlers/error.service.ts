import { Injectable } from '@angular/core';
import { ErrorModel } from './error.model';
import { NotificationService } from '../notification/notification.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private _notificationService: NotificationService) { }

  public throwException = (err: any, source: string, notification: string) => {
    const error: ErrorModel = {
      errType: ErrorModel.name,
      innerException: err,
      source: source,
      notification: notification
    };

    this._notificationService.showError(error.notification);
    throw error;
  };
}