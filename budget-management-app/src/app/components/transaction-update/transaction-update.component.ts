import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TransactionDto } from '../../models/transactionDto';
import { TransactionService } from '../../services/transaction.service';

@Component({
  selector: 'app-transaction-update',
  templateUrl: './transaction-update.component.html',
  styleUrls: ['./transaction-update.component.css'],
})
export class TransactionUpdateComponent {
  transactionForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<TransactionUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public transactioDto: TransactionDto,
    private fb: FormBuilder,
    private transactioService: TransactionService
  ) {
    this.transactionForm = this.fb.group({
      amount: [transactioDto.Amount, [Validators.required]],
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
    if (this.transactionForm.valid) {
      const newAmount = this.transactionForm.value.amount;
      console.log(this.transactioDto);
      this.transactioService
        .updateTransaction(this.transactioDto.TransactionId, newAmount)
        .subscribe(
          (updatedTransaction) => {
            this.dialogRef.close(updatedTransaction);
          },
          (error) => {
            console.error('Error updating transaction:', error);
          }
        );
    }
  }

  closePopup() {
    this.dialogRef.close();
  }
}
