import { Category } from '../enums/category';

export class TransactionDto {
  TransactionId!: string;
  Category!: Category;
  Amount!: number;
  Description!: string;
  TransactionProcessingTime!: string;
}
