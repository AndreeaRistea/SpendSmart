import { Observable } from 'rxjs';
import { HttpService } from './htttp.service';
import { UserDto } from '../models/userDto';
import { Injectable } from '@angular/core';
import { LoginDto } from '../models/loginDto';
import { SignupDto } from '../models/signupDto';
import { CodeLoginRequestDto } from '../models/codeLoginRequestDto';
import { ResetPasswordDto } from '../models/resetPasswordDto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpService: HttpService) {}

  register(user: UserDto): Observable<SignupDto> {
    return this.httpService.post('User/Register', user);
  }

  login(credentials: {
    email: string;
    password: string;
  }): Observable<LoginDto> {
    return this.httpService.post('User/Login', credentials);
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  sendResetPassCode(email: string): Observable<boolean> {
    return this.httpService.post('User/send-reset-code', { email });
  }

  confirmCode(
    email: string,
    codeResetPassword: string
  ): Observable<CodeLoginRequestDto> {
    return this.httpService.post('User/confirm-reset-code', {
      email: email,
      codeResetPassword: codeResetPassword,
    });
  }

  changePassword(
    email: string,
    password: string
  ): Observable<ResetPasswordDto> {
    return this.httpService.put('User/password', {
      email: email,
      password: password,
    });
  }
}
