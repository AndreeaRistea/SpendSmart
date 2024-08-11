import { Component, OnInit } from '@angular/core';
import { UserDetailsDto } from '../../models/userDetailsDto';
import { ProfessionDisplay } from '../../enums/professionDisplay';
import { Option } from '../../helpers/option';
import { ProfileService } from '../../services/profile.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ProfileCreateComponent } from '../profile-create/profile-create.component';
import { LevelDisplay } from '../../enums/levelDisplay';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  userDetails!: UserDetailsDto;
  profession = ProfessionDisplay;
  level = LevelDisplay;
  userLevelFinancialEducation: Option[] = [];
  userProfession: Option[] = [];
  constructor(
    private profileService: ProfileService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.profileService.getUserDetails().subscribe((details) => {
      this.userDetails = details;
      console.log(this.userDetails);
    });
  }

  openEditPopup(): void {
    if (this.dialog.openDialogs.length == 0) {
      const dialogRef = this.dialog.open(ProfileCreateComponent, {
        width: '400px',
        height: '600px',
        data: this.userDetails,
        panelClass: 'dialog-center',
      });

      dialogRef.afterClosed().subscribe((updatedDetails) => {
        if (updatedDetails) {
          this.profileService.getUserDetails().subscribe((details) => {
            this.userDetails = details;
            console.log(this.userDetails);
          });
        }
      });
    }
  }
}
