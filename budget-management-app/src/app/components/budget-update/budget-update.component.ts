import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BudgetDto } from '../../models/budgetDto';
import { BudgetService } from '../../services/budget.service';

@Component({
  selector: 'app-budget-update',
  templateUrl: './budget-update.component.html',
  styleUrls: ['./budget-update.component.css'],
})
export class BudgetUpdateComponent {
  budgetForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<BudgetUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public budgetDto: BudgetDto,
    private fb: FormBuilder,
    private budgetService: BudgetService
  ) {
    console.log(budgetDto);
    this.budgetForm = this.fb.group({
      percent: [
        budgetDto.Percent,
        [Validators.required, Validators.min(0), Validators.max(100)],
      ],
    });
  }

  ngOnInit(): void {
    this.dialogRef.updatePosition({
      top: `15%`,
      left: `45%`,
      right: `25%`,
      bottom: `65%`,
    });
  }

  onSubmit(): void {
    if (this.budgetForm.valid) {
      const newPercent = this.budgetForm.value.percent;
      this.budgetService.updateBudget(this.budgetDto.Id, newPercent).subscribe(
        (updatedBudget) => {
          this.dialogRef.close(updatedBudget);
        },
        (error) => {
          console.error('Error updating budget:', error);
        }
      );
    }
  }

  closePopup() {
    this.dialogRef.close();
  }
}
