import { Injectable } from '@angular/core';
import { HttpService } from './htttp.service';
import { Observable } from 'rxjs';
import { LessonDto } from '../models/lessonDto';

@Injectable({
  providedIn: 'root',
})
export class LessonService {
  constructor(private httpService: HttpService) {}
  getUserLesson(): Observable<LessonDto[]> {
    return this.httpService.get('Lesson');
  }
}
