import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { Category } from '../enums/category';
import { Observable } from 'rxjs';
import { TransactionDto } from '../models/transactionDto';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  constructor(private httpService: HttpService) {}

  // addTransaction(
  //   category: Category,
  //   amount: Number,
  //   description: string,
  //   transactionProcessingTime: string
  // ) {
  //   return this.httpService.post('Transaction/add', {
  //     category: category,
  //     amount: amount,
  //     description: description,
  //     transactionProcessingTime: transactionProcessingTime,
  //   });
  // }
  addTransaction(transactionData: any): Observable<TransactionDto> {
    return this.httpService.post('Transaction/add', transactionData);
  }
  getTransactionByCategory(category: Category): Observable<TransactionDto[]> {
    return this.httpService.get(`Transaction/${category}`);
  }
  getAllTransactions(): Observable<TransactionDto[]> {
    return this.httpService.get('Transaction');
  }
  updateTransaction(
    transactionId: string,
    amount: number
  ): Observable<TransactionDto> {
    return this.httpService.post(`Transaction/update/${transactionId}`, {
      amount: amount,
      transactionId: transactionId,
    });
  }
  deleteTransacation(transactionId: string): Observable<any> {
    return this.httpService.delete(
      `Transaction/delete/${transactionId}`,
      transactionId
    );
  }
}
