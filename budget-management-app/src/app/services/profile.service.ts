import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { UserDetailsDto } from '../models/userDetailsDto';
import { Observable } from 'rxjs';
import { Profession } from '../enums/profession';
import { Level } from '../enums/level';
import { Currency } from '../enums/currency';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private httpService: HttpService) {}

  updateUserDetails(
    income: number,
    profession: Profession,
    level: Level,
    currency: Currency,
    userId: string
  ): Observable<UserDetailsDto> {
    return this.httpService.post(`User/updateDetails/${userId}`, {
      income: income,
      profession: profession,
      level: level,
      currency: currency,
      userId: userId,
    });
  }

  getUserDetails(): Observable<UserDetailsDto> {
    return this.httpService.get(`User/details`);
  }
}
