import { AfterViewInit, Component, ViewChild } from '@angular/core';

import { Transaction } from '../transaction';
import { TransactionService } from '../transaction.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss'],
})
export class TransactionsComponent implements AfterViewInit {
  displayedColumns: string[] = [
    'quantity',
    'totalAmount',
    'description',
    'transactionDate',
    'settlementDate',
    'symbol',
    'action',
  ];
  dataSource!: MatTableDataSource<Transaction>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  constructor(private transactionService: TransactionService) {}

  ngAfterViewInit(): void {
    console.log('ngAfterViewInit');
    this.getTransactions().subscribe((data) => {
      console.log('setting data source');
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  ngOnInit(): void {
    console.log('ngOnInit');
  }

  getTransactions(): Observable<Transaction[]> {
    console.log('getTransactions');
    return this.transactionService.getTransactions();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  addItem(newItem: string) {
    console.log(`addItem: ${newItem}`);
    this.getTransactions();
  }
}
