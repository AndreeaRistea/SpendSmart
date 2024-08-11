import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { UserDetailsDto } from '../models/userDetailsDto';
import { Observable } from 'rxjs';
import { Profession } from '../enums/profession';
import { Level } from '../enums/level';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private httpService: HttpService) {}

  updateUserDetails(
    income: number,
    profession: Profession,
    level: Level,
    userId: string
  ): Observable<UserDetailsDto> {
    return this.httpService.post(`User/updateDetails/${userId}`, {
      income: income,
      profession: profession,
      level: level,
      userId: userId,
    });
  }

  getUserDetails(): Observable<UserDetailsDto> {
    return this.httpService.get(`User/details`);
  }
}
