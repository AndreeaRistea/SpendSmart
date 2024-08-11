import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  public isAuthenticated = new BehaviorSubject<boolean>(false);

  public constructor() {}

  public get validUser(): boolean {
    const token = localStorage.getItem('token');

    if (token) {
      if (this.isValid(token)) {
        return true;
      }
    }
    return false;
  }

  public get isTokenValid() {
    const token = localStorage.getItem('token');

    return token ? this.isValid(token) : false;
  }

  private isValid(token: string) {
    try {
      const expirationDate = this.getTokenExpirationDate(token);

      return expirationDate > new Date(Date.now());
    } catch {
      return false;
    }
  }

  private getTokenExpirationDate(token: string): Date {
    const decoded = jwtDecode(token);

    if (decoded.exp === undefined) {
      return new Date(0);
    }

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);

    return date;
  }
}
