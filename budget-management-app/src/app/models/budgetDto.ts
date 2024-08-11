import { Category } from '../enums/category';

export class BudgetDto {
  Id!: string;
  Category!: Category;
  Percent!: number;
  Value!: number;
  TotalPercentageSpent!: number;
  TotalValueSpent!: number;
  RestSum!: number;
}
