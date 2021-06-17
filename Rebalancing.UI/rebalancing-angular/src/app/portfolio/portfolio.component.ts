import { Component, OnInit } from '@angular/core';
import { Position } from '../position';
import { PortfolioService } from '../portfolio.service';
import { RebalanceModel } from '../rebalance-model';

import { FormBuilder, FormGroup } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { RebalancePosition } from '../rebalance-position';
import { Observable } from 'rxjs';
import { Transaction } from '../transaction';
import { TransactionExchange } from '../transaction-exchange';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.scss'],
})
export class PortfolioComponent implements OnInit {
  columnsToDisplay: string[] = ['symbol', 'percentOfAccount'];
  positions: Position[] = [];

  rebalanceTransactions: Transaction[] = [];
  rebalanceColumnsToDisplay: string[] = ['symbol', 'totalAmount', 'action'];

  exchangeTransactions: TransactionExchange[] = [];
  //totalPercentage?: number = 0;

  rebalanceForm = this.formBuilder.group({
    additionalInvestment: [0],
    desiredPositions: this.formBuilder.array([]),
  });

  buildPosition(position?: RebalancePosition): FormGroup {
    const val: RebalancePosition = position || {
      symbol: '',
      percentOfAccount: 0,
    };
    return this.formBuilder.group(val);
  }

  get desiredPositions(): FormArray {
    return this.rebalanceForm.get('desiredPositions') as FormArray;
  }

  constructor(
    private portfolioService: PortfolioService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getPositions();

    const rebalanceData: RebalanceModel = {
      additionalInvestment: 0,
      desiredPositions: [
        { symbol: 'FLPSX', percentOfAccount: 0.2 },
        { symbol: 'FPADX', percentOfAccount: 0.15 },
        { symbol: 'FRESX', percentOfAccount: 0.1 },
        { symbol: 'FSPSX', percentOfAccount: 0.15 },
        { symbol: 'FSSNX', percentOfAccount: 0.2 },
        { symbol: 'TBCIX', percentOfAccount: 0.2 },
      ],
    };

    rebalanceData.desiredPositions.forEach((x) => {
      this.addDesiredPosition(x);
    });

    // this.totalPercentage = this.desiredPositions
    //   .getRawValue()
    //   .reduce((a: number, b: RebalancePosition) => {
    //     a + b.percentOfAccount;
    //     console.log(a);
    //     console.log(b);
    //   });
  }

  getPositions() {
    this.portfolioService.getPositions().subscribe((p) => (this.positions = p));

    // this.positions.forEach(
    //   (p) =>
    //     (p.desiredPercentOfAccount = this.rebalanceData.desiredPositions.filter(
    //       (x) => x.symbol === p.symbol
    //     )[0].percentOfAccount)
    // );
  }

  rebalance(rebalanceData: RebalanceModel): Observable<Transaction[]> {
    return this.portfolioService.rebalance(rebalanceData);
  }

  exchange(rebalanceData: RebalanceModel): Observable<TransactionExchange[]> {
    return this.portfolioService.exchange(rebalanceData);
  }

  addDesiredPosition(position?: RebalancePosition): void {
    this.desiredPositions.push(this.buildPosition(position));
  }

  onRebalanceSubmit() {
    console.debug(this.rebalanceForm.value);
    this.rebalance(this.rebalanceForm.value).subscribe(
      (x) => (this.rebalanceTransactions = x)
    );
  }

  onExchangeSubmit() {
    console.debug(this.rebalanceForm.value);
    this.rebalance(this.rebalanceForm.value).subscribe(
      (x) => (this.rebalanceTransactions = x)
    );
    
    this.exchange(this.rebalanceForm.value).subscribe(
      (x) => (this.exchangeTransactions = x)
    );
  }
}
