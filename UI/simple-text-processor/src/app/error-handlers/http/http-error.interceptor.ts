import { HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { ErrorModel } from '../error.model';

export class HttpErrorInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {
          let notification: string = '';
          if (error.error instanceof ErrorEvent) {
            notification = 'Error on client-side.';
          } else {
            notification = 'Error on server-side.'
          }
          return throwError(() => this.buildErrorModel(error, notification));
        })
      )
  }

  private buildErrorModel = (err: any, notification: string): ErrorModel => {
    const errModel: ErrorModel = {
      errType: ErrorModel.name,
      message: this.buildMessage(err),
      code: err.status,
      notification: notification,
      stack: this.buildStack(err)
    };

    return errModel;
  }

  private buildStack = (err: any): string => {
    let stack: string = '';
    if (err.stack) {
      stack = err.stack;
    }
    if (err.error) {
      if (stack.length > 0) {
        stack += `\n${err.error.toString()}`;
      }
      else {
        stack = err.error.toString();
      }
    }
    return stack;
  }

  private buildMessage = (err: any): string => {
    let msg: string = '';
    if (err.message) {
      msg = err.message;
    }
    if (err.error.message) {
      if (msg.length > 0) {
        msg += `\n${err.error.message}`;
      }
      else {
        msg = err.error.message;
      }
    }
    return msg;
  }
}