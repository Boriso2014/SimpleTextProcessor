import { Injectable, ErrorHandler, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorModel } from '../error.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandlerService implements ErrorHandler {
  constructor(private _router: Router, private _ngZone: NgZone) { }

  handleError(error: any) {
    console.warn('***GLOBAL ERROR HANDLER***');
    let errModel: ErrorModel;
    if (error.errType === ErrorModel.name) {
      errModel = error;
      console.error(`NOTIFICATION: ${error.notification}\nERROR: ${error.message}`);
    } else {
      errModel = {
        errType: ErrorModel.name,
        message: error.message,
        stack: error.stack,
        notification: 'Something went wrong. See details below.'
      }
    }
    console.error(error);
    this.navigateToErrorPage(errModel);
  }

  private navigateToErrorPage = (errModel: ErrorModel) => {
    this._ngZone.run(() => this._router.navigateByUrl('error', { state: errModel }));
  }
}