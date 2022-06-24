import { HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

export class HttpErrorInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(0), 
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // Error on client-side
            errorMessage = `Error on client-side: ${error.error.message}`;
          } else {
            // Error on server-side
            errorMessage = `Error on server-side. Status code: ${error.status}\nMessage: ${error.error}`;
          }
          return throwError(() => new Error( errorMessage));
        })
      )
  }
}