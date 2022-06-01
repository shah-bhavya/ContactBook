import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable()
export abstract class BaseService {

  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    
    return throwError(() => {
      return errorMessage;
    });
  }
}
