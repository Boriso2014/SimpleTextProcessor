import { Injectable, ErrorHandler } from '@angular/core';
import { ErrorModel } from '../error.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandlerService implements ErrorHandler {

  constructor() { }

  handleError(error: any) {
    console.warn('***GLOBAL ERROR HANDLER***');
    let notification: string = '';
    if (error.errType === ErrorModel.name) {
      const innerEx = error.innerException;
      const msg: string = innerEx.message
        ? innerEx.message
        : innerEx.toString();
      notification = error.notification;
      console.error(`ERROR: ${msg}, SOURCE: ${error.source}, NOTIFICATION: ${notification}`);
    } else {
      console.error(error);
      // TODO: Go to Error page
    }
  }
}