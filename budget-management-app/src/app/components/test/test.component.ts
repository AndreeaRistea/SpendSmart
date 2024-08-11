import { Component, OnInit } from '@angular/core';
import { TestService } from '../../services/test.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrl: './test.component.css',
})
export class TestComponent implements OnInit {
  message: string = 'initial';

  constructor(private testService: TestService) {}

  ngOnInit(): void {
    this.testService.getMockData().subscribe((res) => {
      console.log(res);
      this.message = res.data;
    });
  }
}
