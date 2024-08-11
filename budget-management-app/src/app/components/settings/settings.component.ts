import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { UserService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
  availableLanguages = ['de', 'en', 'fr', 'ro'];
  constructor(
    public translateService: TranslateService,
    private userService: UserService,
    private router: Router
  ) {}
  ngOnInit(): void {}

  switchLang(lang: string) {
    this.translateService.use(lang);
  }
  logout() {
    this.userService.logout();
    this.router.navigate(['login']);
  }
}
