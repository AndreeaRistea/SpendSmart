import { LevelDisplay } from '../enums/levelDisplay';
import { ProfessionDisplay } from '../enums/professionDisplay';

export class UserDetailsDto {
  UserId: string = '';
  Name: string = '';
  Email: string = '';
  Income: number = 0;
  Profession!: ProfessionDisplay;
  LevelFinancialEducation!: LevelDisplay;
}
