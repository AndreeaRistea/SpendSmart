import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { Observable } from 'rxjs';
import { TestDto } from '../models/testDto';

@Injectable({
  providedIn: 'root',
})
export class TestService {
  constructor(private httpService: HttpService) {}

  getMockData(): Observable<TestDto> {
    return this.httpService.get<TestDto>('test');
  }
}
