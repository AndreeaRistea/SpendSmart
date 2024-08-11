import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { LessonDto } from '../../models/lessonDto';
import { LessonService } from '../../services/lesson.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css'],
})
export class LessonComponent implements OnInit {
  lessons: LessonDto[] = [];
  loading = true;
  error: string | null = null;
  constructor(
    private lessonService: LessonService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.loadLessons();
  }

  loadLessons(): void {
    this.lessonService.getUserLesson().subscribe({
      next: (data) => {
        this.lessons = data;
        this.loading = false;
        console.log(this.lessons);
        console.log([...this.lessons]);
      },
      error: (err) => {
        this.error = 'Failed to load lessons. Please try again later.';
        this.loading = false;
      },
    });
  }

  getCoverImageSrc(imageBytes: string): any {
    const imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
      'data:image/jpg;base64,' + imageBytes
    );
    return imagePath;
  }

  getFileText(fileBytes: string | undefined) {
    if (fileBytes) {
      this.openBase64NewTab(fileBytes);
    }
  }

  base64toBlob(base64Data: string) {
    const sliceSize = 1024;
    const byteCharacters = atob(base64Data);
    const bytesLength = byteCharacters.length;
    const slicesCount = Math.ceil(bytesLength / sliceSize);
    const byteArrays = new Array(slicesCount);

    for (let sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
      const begin = sliceIndex * sliceSize;
      const end = Math.min(begin + sliceSize, bytesLength);

      const bytes = new Array(end - begin);
      for (let offset = begin, i = 0; offset < end; ++i, ++offset) {
        bytes[i] = byteCharacters[offset].charCodeAt(0);
      }
      byteArrays[sliceIndex] = new Uint8Array(bytes);
    }
    return new Blob(byteArrays, { type: 'application/pdf' });
  }

  openBase64NewTab(base64Pdf: string): void {
    const blob = this.base64toBlob(base64Pdf);
    const blobUrl = URL.createObjectURL(blob);
    window.open(blobUrl);
  }
}
