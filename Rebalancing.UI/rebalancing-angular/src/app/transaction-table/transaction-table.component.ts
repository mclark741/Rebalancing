import { Component, OnInit, Input } from '@angular/core';
import { Transaction } from '../transaction';

@Component({
  selector: 'app-transaction-table',
  templateUrl: './transaction-table.component.html',
  styleUrls: ['./transaction-table.component.scss'],
})
export class TransactionTableComponent implements OnInit {
  @Input() transactions?: Transaction[];
  @Input() columnsToDisplay: string[] = [];

  constructor() {}

  ngOnInit(): void {}
}
