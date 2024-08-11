import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor(public translate: TranslateService, private router: Router) {
    translate.addLangs(['en', 'ro', 'fr', 'de']);
    translate.setDefaultLang('en');
  }
  title = 'budget-management-app';
  toggle($event: any) {
    console.log($event);
  }
  isLoginRoute(): boolean {
    return this.router.url === '/login';
  }
}
