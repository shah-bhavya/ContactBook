import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ILoginModel } from 'src/app/shared/models/loginModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiURL: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  Login(loginModel: ILoginModel): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.post(this.apiURL + 'login', loginModel, httpOptions).pipe(
      map(response => {
        return response;
      })
    );
  }
}
