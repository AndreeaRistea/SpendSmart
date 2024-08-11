import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import ValidateForm from '../../helpers/validateForm';
import { Profession } from '../../enums/profession';
import { ProfileService } from '../../services/profile.service';
import { Option } from '../../helpers/option';
import { Level } from '../../enums/level';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  loginForm!: FormGroup;
  signupForm!: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  showAuth = true;

  detailsForm!: FormGroup;
  dropdownValues: Option[] = [];
  levelDropdownVlues: Option[] = [];

  constructor(
    private fb: FormBuilder,
    private user: UserService,
    private router: Router,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern('[a-zA-Z ]*[0-9]*'),
        ],
      ],
    });

    this.signupForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern('[a-zA-Z ]*[0-9]*'),
        ],
      ],
    });

    this.dropdownValues = Object.keys(Profession).map((key) => {
      return new Option({ name: key, value: key });
    });

    this.levelDropdownVlues = Object.keys(Level).map((key) => {
      return new Option({ name: key, value: key });
    });

    console.log(this.dropdownValues);
    this.detailsForm = this.fb.group({
      income: [''],
      profession: [this.dropdownValues[0]],
      levelFinancialEducation: [this.levelDropdownVlues[0]],
    });
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  onLogin() {
    if (this.loginForm.valid) {
      const credentials = {
        email: this.loginForm.controls['email'].value,
        password: this.loginForm.controls['password'].value,
      };
      this.user.login(credentials).subscribe({
        next: (res) => {
          console.log(res);
          this.loginForm.reset();
          localStorage.setItem('userId', res.Id);
          localStorage.setItem('token', res.Token);
          this.router.navigate(['home']);
        },
        error: (err) => {
          console.log(err);
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }

  onDetailsSave() {
    if (this.signupForm.valid) {
      console.log(this.showAuth);

      this.user.register(this.signupForm.value).subscribe({
        next: (signupResponse) => {
          console.log(signupResponse);
          this.signupForm.reset();

          if (this.detailsForm.valid) {
            console.log;
            const income = this.detailsForm.controls['income'].value;
            const profession = this.detailsForm.controls['profession'].value;
            const level =
              this.detailsForm.controls['levelFinancialEducation'].value;
            this.profileService
              .updateUserDetails(
                income,
                profession.value,
                level.value,
                signupResponse.Id
              )
              .subscribe({
                next: (resp) => {
                  console.log(resp);
                  localStorage.setItem('userId', signupResponse.Id);
                  localStorage.setItem('token', signupResponse.Token);
                  this.router.navigate(['home']);
                },
                error: (err) => {
                  console.log(err);
                },
              });
          }
        },
      });
    }
  }

  onSignUp() {
    if (this.signupForm.valid) {
      this.showAuth = !this.showAuth;
      console.log(this.showAuth);
    } else {
      ValidateForm.validateAllFormFields(this.signupForm);
    }
  }

  navigateToForgotPassword() {
    this.router.navigate(['/reset-password']);
  }

  @Output() onToggleLogo: EventEmitter<LogoToggle> = new EventEmitter();
  collapsed = true;
  screenWidth = 0;

  toggleCollapse(): void {
    this.collapsed = !this.collapsed;
    this.onToggleLogo.emit({
      collapsed: this.collapsed,
      screenWidth: this.screenWidth,
    });
  }
}
interface LogoToggle {
  screenWidth: number;
  collapsed: boolean;
}
