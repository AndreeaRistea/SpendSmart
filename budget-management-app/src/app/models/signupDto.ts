import { Currency } from '../enums/currency';

export class SignupDto {
  Id: string = '';
  Name: string = '';
  Email: string = '';
  Password: string = '';
  Token: string = '';
  RefreshToken: string = '';
  Currency!: Currency;
}
