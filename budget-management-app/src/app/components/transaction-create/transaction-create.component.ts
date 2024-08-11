import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from '../../enums/category';
import { Option } from '../profile-create/profile-create.component';
import { TransactionService } from '../../services/transaction.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-transaction-create',
  templateUrl: './transaction-create.component.html',
  styleUrls: ['./transaction-create.component.css'],
})
export class TransactionCreateComponent implements OnInit {
  transactionForm!: FormGroup;
  category!: Category;
  dropdownValues: Option[] = [];
  constructor(
    private transactionService: TransactionService,
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.dropdownValues = Object.keys(Category).map((key) => {
      return new Option({ name: key, value: key });
    });
    console.log(this.dropdownValues);
    this.transactionForm = this.fb.group({
      category: [this.dropdownValues[0]],
      amount: ['', [Validators.required, Validators.min(1)]],
      description: ['', Validators.required],
      transactionProcessingTime: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      const category = this.transactionForm.controls['category'].value;
      const valueTransaction = this.transactionForm.controls['amount'].value;
      const descript = this.transactionForm.controls['description'].value;
      const data =
        this.transactionForm.controls['transactionProcessingTime'].value;
      const transactionData = {
        category: category.value,
        amount: valueTransaction,
        description: descript,
        transactionProcessingTime: data,
      };

      console.log(transactionData);

      this.transactionService.addTransaction(transactionData).subscribe({
        next: (resp) => {
          console.log(resp);
          this.router.navigate(['transaction']);
        },
        error: (err) => {
          console.log(err);
          if (err.status === 500) {
            this.snackBar.open(
              'No budget allocated for this category',
              'Close',
              {
                duration: 3000,
              }
            );
          } else {
            this.snackBar.open(
              'An error occurred. Please try again later.',
              'Close',
              {
                duration: 3000,
              }
            );
          }
        },
      });
    } else {
      console.warn('Form is invalid');
      console.log(this.transactionForm.value);
    }
  }
}
