import { Component, OnInit, Input } from '@angular/core';
import { Position } from '../position';

@Component({
  selector: 'app-portfolio-table',
  templateUrl: './portfolio-table.component.html',
  styleUrls: ['./portfolio-table.component.scss'],
})
export class PortfolioTableComponent implements OnInit {
  @Input() positions?: Position[];
  columnsToDisplay: string[] = [
    'symbol',
    'currentValue',
    'percentOfAccount',
    'quantity',
  ];

  constructor() {}

  ngOnInit(): void {}
}
