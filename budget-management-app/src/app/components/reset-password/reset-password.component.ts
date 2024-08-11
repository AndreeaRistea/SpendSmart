import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/auth.service';
import { CodeLoginRequestDto } from '../../models/codeLoginRequestDto';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  resetForm!: FormGroup;
  codeForm!: FormGroup;
  newPasswordForm!: FormGroup;
  step: number = 1; // 1: email, 2: code, 3: new password

  constructor(
    private fb: FormBuilder,
    private user: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.resetForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });

    this.codeForm = this.fb.group({
      email: ['', Validators.required],
      codeResetPassword: ['', Validators.required],
    });

    this.newPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  sendResetCode() {
    if (this.resetForm.valid) {
      const email = this.resetForm.controls['email'].value;
      this.user.sendResetPassCode(email).subscribe({
        next: (res) => {
          console.log(res);
          this.step = 2;
          this.codeForm.controls['email'].setValue(email);
          this.newPasswordForm.controls['email'].setValue(email);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  confirmCode() {
    if (this.codeForm.valid) {
      const email = this.codeForm.controls['email'].value;
      const code = this.codeForm.controls['codeResetPassword'].value;
      this.user.confirmCode(email, code).subscribe({
        next: (res: CodeLoginRequestDto) => {
          this.step = 3;
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  changePassword() {
    if (this.newPasswordForm.valid) {
      const email = this.newPasswordForm.controls['email'].value;
      const password = this.newPasswordForm.controls['password'].value;
      this.user.changePassword(email, password).subscribe({
        next: (res) => {
          this.router.navigate(['login']);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
