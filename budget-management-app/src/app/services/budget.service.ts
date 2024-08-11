import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { BudgetDto } from '../models/budgetDto';
import { Observable } from 'rxjs';
import { Category } from '../enums/category';
import { BudgetDisplayDto } from '../models/budgetDisplayDto';

@Injectable({
  providedIn: 'root',
})
export class BudgetService {
  constructor(private httpService: HttpService) {}
  createBudgetOnCategory(category: Category, percent: Number) {
    return this.httpService.post('Budget', {
      category: category,
      percent: percent,
    });
  }

  getAllBudgets(): Observable<BudgetDto[]> {
    return this.httpService.get('Budget');
  }

  getExistingBudgets(): Observable<BudgetDto[]> {
    return this.httpService.get('Budget/allExisting');
  }

  getBudgetByCategory(category: Category): Observable<BudgetDto> {
    return this.httpService.get(`Budget/${category}`);
  }

  deleteBudget(budgetId: string): Observable<any> {
    return this.httpService.delete(`Budget/delete/  ${budgetId}`, budgetId);
  }
  updateBudget(budgetId: string, newPercent: number): Observable<BudgetDto> {
    return this.httpService.post(`Budget/updateBudget/${budgetId}`, {
      percent: newPercent,
      Id: budgetId,
    });
  }
}
