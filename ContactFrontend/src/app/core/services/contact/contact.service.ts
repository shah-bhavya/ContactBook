import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
import { ContactResponseModel } from 'src/app/shared/models/contactResponseModel';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from '../base.service';
import { IContactRequestModel } from 'src/app/shared/models/contactRequestModel';


@Injectable({
  providedIn: 'root'
})
export class ContactService extends BaseService {

  private apiURL: string = environment.apiURL;

  constructor(private http: HttpClient) {
    super();
  }


GetAllContacts()
{
  return this.http.get<ContactResponseModel[]>(this.apiURL + 'contact').pipe(
    map(response => {
      return response;
    })
  )
}

GetContactById(Id)
{
  return this.http.get<ContactResponseModel>(this.apiURL + 'contact/'+ Id).pipe(
    map(response => {
      return response;
    })
  )
}
  
// CreateContacts(ContactRequestModel: IContactRequestModel): Observable<any>
// {
//   const httpOptions = {
//     headers: new HttpHeaders({
//       'Content-Type': 'application/json'
//     })
//   }

//   return this.http.post(this.apiURL + 'contact', ContactRequestModel, httpOptions).pipe(
//     map(response => {
//       return response;
//     })
//   );
// }

CreateContacts(formData: FormData): Observable<any>
{
  return this.http.post(this.apiURL + 'contact', formData).pipe(
    map(response => {
      return response;
    })
  );
}

UpdateContacts(Id, ContactModel: FormData): Observable<any>
{
  return this.http.put(this.apiURL + 'contact/'+Id, ContactModel).pipe(
    map(response => {
      return response;
    })
  );
}

DeleteContacts(Id): Observable<any>
{
  return this.http.delete(this.apiURL + 'contact/'+Id).pipe(
    map(response => {
      return response;
    })
  );
}

UpdateFavourite(Id): Observable<any>
{
  return this.http.get(this.apiURL + 'contact/favourite/' + Id).pipe(
    map(response => {
      return response;
    })
  );
}
  
}
