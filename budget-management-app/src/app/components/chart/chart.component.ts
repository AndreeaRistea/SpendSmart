import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BudgetService } from '../../services/budget.service';
import { Chart, ChartData, ChartOptions, ChartType, LabelItem } from 'chart.js';
import { BudgetDto } from '../../models/budgetDto';
import { Category } from '../../enums/category';
import { Option } from '../../helpers/option';
import { CategoryDisplay } from '../../enums/categoryDisplay';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit {
  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: string[] = [];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;

  public barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: [
      { data: [], label: 'Initial Budget' },
      { data: [], label: 'Spent Budget' },
    ],
  };

  constructor(private budgetService: BudgetService) {}
  ngOnInit(): void {
    this.loadBudgets();
  }

  loadBudgets(): void {
    this.budgetService.getAllBudgets().subscribe((budgets: BudgetDto[]) => {
      this.barChartLabels = Object.keys(Category).map((key) => {
        return new Option({ name: key, value: key }).name;
      });
      console.log(this.barChartLabels);
      this.barChartData.labels = this.barChartLabels;
      this.barChartData.datasets[0].data = budgets.map((b) => b.Percent);
      this.barChartData.datasets[1].data = budgets.map(
        (b) => b.TotalPercentageSpent
      );
      console.log(this.barChartData);
    });
  }
}
