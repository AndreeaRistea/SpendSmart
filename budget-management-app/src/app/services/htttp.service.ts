import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl = 'http://localhost:5220/api/';
  constructor(private httpClient: HttpClient) {}

  get<T>(url: string) {
    const token = localStorage.getItem('token');

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    return this.httpClient.get<T>(this.apiUrl + url, {
      headers: headers,
    });
  }

  post<T>(url: string, data: any): Observable<T> {
    const token = localStorage.getItem('token');

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    console.log(token);
    return this.httpClient.post<T>(this.apiUrl + url, data, {
      headers: headers,
    });
  }

  put<T>(url: string, data: any): Observable<T> {
    const token = localStorage.getItem('token');

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    console.log(token);
    return this.httpClient.put<T>(this.apiUrl + url, data, {
      headers: headers,
    });
  }

  delete<T>(url: string, data: any): Observable<T> {
    const token = localStorage.getItem('token');

    let headers = new HttpHeaders();
    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    console.log(token);
    return this.httpClient.delete<T>(this.apiUrl + url, {
      headers: headers,
    });
  }
}
