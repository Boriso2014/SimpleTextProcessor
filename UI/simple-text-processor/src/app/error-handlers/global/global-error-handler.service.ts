import { Injectable, ErrorHandler } from '@angular/core';
import { ErrorModel } from '../error.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandlerService implements ErrorHandler {

  constructor() { }

  handleError(error: any) {
    console.error('***GLOBAL ERROR HANDLER***');
    let notification: string = '';
    if (error.errType === ErrorModel.name) {
      const innerEx = error.innerException;
      const msg: string = innerEx.message
        ? innerEx.message
        : innerEx.toString();
      console.error(`ERROR: ${msg}, SOURCE: ${error.source}, NOTIFICATION: ${notification}`);
      notification = error.notification;
    } else {
      console.error(error);
      // TODO: Go to Error page
    }
  }
}