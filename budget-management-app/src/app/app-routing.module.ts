import { Route, RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthComponent } from './components/auth/auth.component';
import { authGuard } from './guards/auth.guard';
import { SettingsComponent } from './components/settings/settings.component';
import { BudgetCreateComponent } from './components/budget-create/budget-create.component';
import { BudgetComponent } from './components/budget/budget.component';
import { ProfileCreateComponent } from './components/profile-create/profile-create.component';
import { ProfileComponent } from './components/profile/profile.component';
import { TransactionCreateComponent } from './components/transaction-create/transaction-create.component';
import { TransactionComponent } from './components/transaction/transaction.component';
import { ChartComponent } from './components/chart/chart.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { LessonComponent } from './components/lesson/lesson.component';
import { StartPageComponent } from './components/start-page/start-page.component';

const routes: Routes = [
  {
    path: '',
    ///component: AuthComponent,
    redirectTo: 'login',
    pathMatch: 'full',
  },
  { path: 'login', component: AuthComponent },
  { path: 'home', component: StartPageComponent, canActivate: [authGuard] },
  {
    path: 'settings',
    component: SettingsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'budgets',
    component: BudgetComponent,
    canActivate: [authGuard],
  },
  {
    path: 'budget-create',
    component: BudgetCreateComponent,
    canActivate: [authGuard],
  },
  {
    path: 'profile-create/:userId',
    component: ProfileCreateComponent,
    canActivate: [authGuard],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [authGuard],
  },
  {
    path: 'transaction-create',
    component: TransactionCreateComponent,
    canActivate: [authGuard],
  },
  {
    path: 'transaction',
    component: TransactionComponent,
    canActivate: [authGuard],
  },
  {
    path: 'chart',
    component: ChartComponent,
    canActivate: [authGuard],
  },
  {
    path: 'lessons',
    component: LessonComponent,
    canActivate: [authGuard],
  },
  {
    path: 'reset-password',
    component: ResetPasswordComponent,
  },
  {
    path: '**',
    redirectTo: 'test',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
