import { Currency } from '../enums/currency';
import { CurrencyDisplay } from '../enums/currencyDisplay';
import { LevelDisplay } from '../enums/levelDisplay';
import { ProfessionDisplay } from '../enums/professionDisplay';

export class UserDetailsDto {
  UserId: string = '';
  Name: string = '';
  Email: string = '';
  Income: number = 0;
  Profession!: ProfessionDisplay;
  LevelFinancialEducation!: LevelDisplay;
  Currency!: Currency;
}
