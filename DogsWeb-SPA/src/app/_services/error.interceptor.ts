import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
   return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401 && error.status === 400 || error.status === 201){
          return throwError(error.statusText);
        }
        if(error instanceof HttpErrorResponse){
          const applicationError = error.headers.get('ApplicationError');
          if(applicationError){
            return throwError(applicationError);
          }
          const serverError = error.error;
          let modalStateError = '';
          if(serverError.value && typeof serverError.value === 'object'){
            for (const key in serverError.value){
              if (serverError.value[key]){
                modalStateError += serverError.value[key] + '\n';
              }
            }
          }else if(serverError.errors && typeof serverError.errors === 'object'){
            for (const key in serverError.errors){
              if (serverError.errors[key]){
                modalStateError += serverError.errors[key] + '\n';
              }
            }
          }

          return throwError(modalStateError || serverError || 'Server Error');
        }
      })
   );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};

