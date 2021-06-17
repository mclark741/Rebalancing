import { Component, OnInit, Input } from '@angular/core';
import { TransactionExchange } from '../transaction-exchange';

@Component({
  selector: 'app-transaction-exchange-table',
  templateUrl: './transaction-exchange-table.component.html',
  styleUrls: ['./transaction-exchange-table.component.scss'],
})
export class TransactionExchangeTableComponent implements OnInit {
  @Input() transactions?: TransactionExchange[];
  columnsToDisplay: string[] = ['sellSymbol', 'buySymbol', 'totalAmount'];
  constructor() {}

  ngOnInit(): void {}
}
