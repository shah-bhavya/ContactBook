import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isLoggedIn() {
    const token = localStorage.getItem('token'); // get token from local storage
    if (!token)
      return false;

    return !this.tokenExpired(token);
    //const payload = atob(token.split('.')[1]); // decode payload of token
    //const parsedPayload = JSON.parse(payload); // convert payload into an Object
  }

  private tokenExpired(token: string) {
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }
}
