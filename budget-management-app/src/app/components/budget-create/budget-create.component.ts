import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Category } from '../../enums/category';
import { BudgetService } from '../../services/budget.service';
import { Router } from '@angular/router';
import { Option } from '../../helpers/option';

@Component({
  selector: 'app-budget-create',
  templateUrl: './budget-create.component.html',
  styleUrls: ['./budget-create.component.css'],
})
export class BudgetCreateComponent implements OnInit {
  budgetForm!: FormGroup;
  category!: Category;
  budgetCategories: Option[] = [];
  constructor(
    private budgetService: BudgetService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.budgetCategories = Object.keys(Category).map((key) => {
      return new Option({ name: key, value: key });
    });

    console.log(this.budgetCategories);
    this.budgetForm = this.fb.group({
      percent: [
        '',
        [Validators.required, Validators.min(0), Validators.max(100)],
      ],
      budgetCategory: [this.budgetCategories[0]],
    });
  }

  onSubmit(): void {
    if (this.budgetForm.valid) {
      const percent = this.budgetForm.controls['percent'].value;
      const budgetCategory = this.budgetForm.controls['budgetCategory'].value;

      this.budgetService
        .createBudgetOnCategory(budgetCategory.value, percent)
        .subscribe({
          next: (resp) => {
            console.log(resp);
            this.router.navigate(['budgets']);
          },
          error: (err) => {
            console.log(err);
          },
        });
    } else {
      console.warn('Form is invalid');
      console.log(this.budgetForm.value);
    }
  }
}
