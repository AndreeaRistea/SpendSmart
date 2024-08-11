import { Component } from '@angular/core';
import { TransactionDto } from '../../models/transactionDto';

import { Option } from '../../helpers/option';
import { TransactionService } from '../../services/transaction.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../enums/category';
import { OnInit } from '@angular/core';
import { CategoryDisplay } from '../../enums/categoryDisplay';
import { MatDialog } from '@angular/material/dialog';
import { TransactionUpdateComponent } from '../transaction-update/transaction-update.component';
import { ConfirmDeleteDialogComponent } from '../confirm-delete-dialog/confirm-delete-dialog.component';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css'],
})
export class TransactionComponent implements OnInit {
  transactions: TransactionDto[] = [];
  category!: Category;
  categoryDisplay = CategoryDisplay;
  dropdownValues: Option[] = [];

  constructor(
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.transactionService.getAllTransactions().subscribe((transactions) => {
      this.transactions = transactions;

      console.log(this.transactions);
    });
  }
  addNewTransaction() {
    this.router.navigate(['transaction-create']);
  }
  private deleteTransaction(transactionId: string) {
    this.transactionService.deleteTransacation(transactionId).subscribe(() => {
      this.transactionService.getAllTransactions().subscribe((transactions) => {
        this.transactions = transactions;
      });
    });
  }
  confirmDeleteTransaction(transactionId: string): void {
    const dialogRef = this.dialog.open(ConfirmDeleteDialogComponent, {
      width: '400px',
      data: {
        title: 'transaction.ConfirmDeleteTitle',
        message: 'transaction.ConfirmDeleteMessage',
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteTransaction(transactionId);
      }
    });
  }
  loadTransaction(): void {
    this.transactionService.getAllTransactions().subscribe((transactions) => {
      this.transactions = transactions;
      console.log(this.transactions);
    });
  }
  openUpdatePopup(transaction: TransactionDto): void {
    console.log(transaction);
    const dialogRef = this.dialog.open(TransactionUpdateComponent, {
      width: '400px',
      data: transaction,
    });
    dialogRef.afterClosed().subscribe((updatedTransaction) => {
      if (updatedTransaction) {
        this.loadTransaction();
      }
    });
  }
}
