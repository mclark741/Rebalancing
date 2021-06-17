import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { Position } from './position';
import { RebalanceModel } from './rebalance-model';
import { Transaction } from './transaction';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';
import { AppConfig } from './app-config';

//mocks
import { POSITIONS } from './mock-portfolio';
import { REBALANCE_TRANSACTIONS } from './mock-rebalance-transactions';

@Injectable({
  providedIn: 'root',
})
export class PortfolioService {
  private apiUrl = `${AppConfig.settings.apiUrl}/portfolio`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  getPositions(): Observable<Position[]> {
    return this.http.get<Position[]>(this.apiUrl).pipe(
      tap((_) => this.log('fetched positions')),
      catchError(this.handleError<Position[]>('getPositions', []))
    );
  }

  rebalance(data: RebalanceModel): Observable<any> {
    return this.http
      .put(
        `${this.apiUrl}/${data.additionalInvestment}`,
        data.desiredPositions,
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log('fetched rebalanced transactions')),
        catchError(this.handleError<any>('rebalance'))
      );
  }

  exchange(data: RebalanceModel): Observable<any> {
    return this.http
      .put(
        `${this.apiUrl}/exchange/${data.additionalInvestment}`,
        data.desiredPositions,
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log('fetched rebalanced transactions')),
        catchError(this.handleError<any>('rebalance'))
      );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`PortfolioService: ${message}`);
  }
}
