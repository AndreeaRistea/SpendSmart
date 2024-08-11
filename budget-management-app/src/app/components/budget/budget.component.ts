import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../../services/budget.service';

import { Router } from '@angular/router';
import { CategoryDisplay } from '../../enums/categoryDisplay';
import { Option } from '../../helpers/option';
import { BudgetDto } from '../../models/budgetDto';
import { MatDialog } from '@angular/material/dialog';
import { BudgetUpdateComponent } from '../budget-update/budget-update.component';
import { ConfirmDeleteDialogComponent } from '../confirm-delete-dialog/confirm-delete-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrls: ['./budget.component.css'],
})
export class BudgetComponent implements OnInit {
  budgets: BudgetDto[] = [];
  Category = CategoryDisplay;
  budgetCategories: Option[] = [];
  showPercent: boolean = true;
  restSum!: number;
  shownValue: number = 0;
  editBudgetId: string | null = null;

  constructor(
    private budgetService: BudgetService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.budgetService.getExistingBudgets().subscribe((budgets) => {
      this.budgets = budgets;

      console.log(this.budgets);
    });
  }

  loadBudgets(): void {
    this.budgetService.getExistingBudgets().subscribe((budgets) => {
      this.budgets = budgets;
      console.log(this.budgets);
    });
  }

  addNewBudget() {
    this.router.navigate(['budget-create']);
  }

  toggleDisplay() {
    this.showPercent = !this.showPercent;
  }

  private deleteBudget(budgetId: string) {
    this.budgetService.deleteBudget(budgetId).subscribe({
      next: () => {
        this.loadBudgets();
      },
      error: (err) => {
        this.snackBar.open(
          err.error.message || 'Failed to delete budget.',
          'Close',
          {
            duration: 3000,
          }
        );
      },
    });
  }

  confirmDeleteBudget(budgetId: string): void {
    const dialogRef = this.dialog.open(ConfirmDeleteDialogComponent, {
      width: '400px',
      data: {
        title: 'budget.ConfirmDeleteTitle',
        message: 'budget.ConfirmDeleteMessage',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBudget(budgetId);
      }
    });
  }

  openUpdatePopup(budget: BudgetDto): void {
    const dialogRef = this.dialog.open(BudgetUpdateComponent, {
      width: '400px',
      data: budget,
    });

    dialogRef.afterClosed().subscribe((updatedBudget) => {
      if (updatedBudget) {
        this.loadBudgets();
      }
    });
  }

  round(num: number) {
    console.log(num);
    return num.toFixed(2);
  }

  getRemainingPercentage(): number {
    const totalAllocated = this.budgets.reduce(
      (sum, budget) => sum + budget.Percent,
      0
    );
    return Math.max(0, 100 - totalAllocated);
  }
}
