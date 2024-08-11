import { NgModule } from '@angular/core';
import { TestComponent } from './components/test/test.component';
import { AppComponent } from './app.component';
import { HttpService } from './services/htttp.service';
import { TestService } from './services/test.service';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserService } from './services/auth.service';
import { AuthComponent } from './components/auth/auth.component';
import { AppRoutingModule } from './app-routing.module';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SettingsComponent } from './components/settings/settings.component';
import { MatIconModule } from '@angular/material/icon';
import { BudgetService } from './services/budget.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { BudgetComponent } from './components/budget/budget.component';
import { BudgetCreateComponent } from './components/budget-create/budget-create.component';
import { ProfileCreateComponent } from './components/profile-create/profile-create.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProfileService } from './services/profile.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { TransactionCreateComponent } from './components/transaction-create/transaction-create.component';
import { TransactionService } from './services/transaction.service';
import { TransactionComponent } from './components/transaction/transaction.component';
import {
  BaseChartDirective,
  provideCharts,
  withDefaultRegisterables,
} from 'ng2-charts';
import { ChartComponent } from './components/chart/chart.component';
import { BudgetUpdateComponent } from './components/budget-update/budget-update.component';
import { TransactionUpdateComponent } from './components/transaction-update/transaction-update.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LessonComponent } from './components/lesson/lesson.component';
import { ConfirmDeleteDialogComponent } from './components/confirm-delete-dialog/confirm-delete-dialog.component';
import { StartPageComponent } from './components/start-page/start-page.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    AuthComponent,
    SidenavComponent,
    SettingsComponent,
    BudgetComponent,
    BudgetCreateComponent,
    ProfileCreateComponent,
    ProfileComponent,
    TransactionCreateComponent,
    TransactionComponent,
    ChartComponent,
    BudgetUpdateComponent,
    TransactionUpdateComponent,
    ResetPasswordComponent,
    LessonComponent,
    ConfirmDeleteDialogComponent,
    StartPageComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    BaseChartDirective,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient],
      },
    }),
    MatSnackBarModule,
  ],
  exports: [
    AppComponent,
    TestComponent,
    AuthComponent,
    SidenavComponent,
    SettingsComponent,
    BudgetComponent,
    BudgetCreateComponent,
    ProfileCreateComponent,
    ProfileComponent,
    TransactionCreateComponent,
    TransactionComponent,
    ChartComponent,
    BudgetUpdateComponent,
    TransactionUpdateComponent,
    ResetPasswordComponent,
    LessonComponent,
    ConfirmDeleteDialogComponent,
    StartPageComponent,
  ],
  providers: [
    HttpService,
    TestService,
    UserService,
    BudgetService,
    ProfileService,
    TransactionService,
    provideAnimationsAsync(),
    provideCharts(withDefaultRegisterables()),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
