import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  OnInit,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Profession } from '../../enums/profession';
import { ProfileService } from '../../services/profile.service';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserDetailsDto } from '../../models/userDetailsDto';
import { Level } from '../../enums/level';
import { ProfessionDisplay } from '../../enums/professionDisplay';
import { LevelDisplay } from '../../enums/levelDisplay';
import { Currency } from '../../enums/currency';
import { CurrencyDisplay } from '../../enums/currencyDisplay';

@Component({
  selector: 'app-profile-create',
  templateUrl: './profile-create.component.html',
  styleUrls: ['./profile-create.component.css'],
})
export class ProfileCreateComponent {
  detailsForm!: FormGroup;
  profession!: Profession;
  LevelFinancialEducation!: Level;
  Currency!: Currency;
  levelDropdownValues: Option[] = [];
  professionDropdownValues: Option[] = [];
  currencyDropdownValues: Option[] = [];
  constructor(
    public dialogRef: MatDialogRef<ProfileCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public userDetails: UserDetailsDto,
    private profileService: ProfileService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.professionDropdownValues = Object.keys(Profession).map((key) => {
      return new Option({ name: key, value: key });
    });
    this.levelDropdownValues = Object.keys(Level).map((key) => {
      return new Option({ name: key, value: key });
    });
    this.currencyDropdownValues = Object.keys(Currency).map((key) => {
      return new Option({ name: key, value: key });
    });
    console.log(userDetails);
    this.detailsForm = this.fb.group({
      income: [userDetails.Income, Validators.required],
      currency: [userDetails.Currency, Validators.required],
      profession: [
        ProfessionDisplay[userDetails.Profession],
        Validators.required,
      ],
      levelFinancialEducation: [
        LevelDisplay[userDetails.LevelFinancialEducation],
        Validators.required,
      ],
    });
  }
  ngOnInit(): void {
    this.dialogRef.updatePosition({
      top: `5%`,
      left: `45%`,
      right: `25%`,
      bottom: `60%`,
    });
  }

  onSubmit(): void {
    if (this.detailsForm.valid) {
      const updatedDetails = this.detailsForm.value;
      const userID = localStorage.getItem('userId');
      if (userID)
        this.profileService
          .updateUserDetails(
            updatedDetails.income,
            updatedDetails.profession,
            updatedDetails.levelFinancialEducation,
            updatedDetails.currency,
            userID
          )
          .subscribe({
            next: (resp) => {
              localStorage.removeItem('currency');
              localStorage.setItem('currency', resp.Currency);
              console.log(resp);
              console.log(userID);
              this.dialogRef.close(this.userDetails);
            },
            error: (err) => {
              console.log(err);
            },
          });
    } else {
      console.warn('Form is invalid');
      console.log(this.detailsForm.value);
    }
  }

  Closepopup() {
    this.dialogRef.close('Closing from function');
  }
}

export class Option {
  name!: string;
  value!: string;

  constructor(init?: Partial<Option>) {
    Object.assign(this, init);
  }
}
