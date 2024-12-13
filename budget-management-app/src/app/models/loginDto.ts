import { Currency } from '../enums/currency';

export class LoginDto {
  Id: string = '';
  Name: string = '';
  Email: string = '';
  Password: string = '';
  Token: string = '';
  RefreshToken: string = '';
  Currency!: Currency;
}
