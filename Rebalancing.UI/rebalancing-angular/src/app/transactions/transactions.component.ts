import { Component, OnInit } from '@angular/core';

import { Transaction } from '../transaction';
import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss'],
})
export class TransactionsComponent implements OnInit {
  transactions: Transaction[] = [];

  columnsToDisplay: string[] = [
    'quantity',
    'totalAmount',
    'description',
    'transactionDate',
    'settlementDate',
    'symbol',
    'action',
  ];
  constructor(private transactionService: TransactionService) {}

  ngOnInit(): void {
    this.getTransactions();
  }

  getTransactions() {
    this.transactionService
      .getTransactions()
      .subscribe((t) => (this.transactions = t));
  }

  addItem(newItem: string) {
    console.log(`addItem: ${newItem}`);
    this.transactions = [];
    this.getTransactions();
  }
}
