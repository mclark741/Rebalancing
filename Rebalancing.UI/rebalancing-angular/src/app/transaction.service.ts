import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { Transaction } from './transaction';
import { TRANSACTIONS } from './mock-transactions';

import {
  HttpClient,
  HttpEvent,
  HttpEventType,
  HttpProgressEvent,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';

import { catchError, last, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';
import { AppConfig } from './app-config';

@Injectable({
  providedIn: 'root',
})

export class TransactionService {
  private apiUrl = `${AppConfig.settings.apiUrl}/transaction`;

  constructor(
    private http: HttpClient,
    private messageService: MessageService,
    private config: AppConfig
  ) {}

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl).pipe(
      tap((_) => this.log('fetched transactions')),
      catchError(this.handleError<Transaction[]>('getTransactions', []))
    );
  }

  // If uploading multiple files, change to:
  // upload(files: FileList) {
  //   const formData = new FormData();
  //   files.forEach(f => formData.append(f.name, f));
  //   new HttpRequest('POST', '/upload/file', formData, {reportProgress: true});
  //   ...
  // }

  upload(file: File) {
    // Create the request object that POSTs the file to an upload endpoint.
    // The `reportProgress` option tells HttpClient to listen and return
    // XHR progress events.
    const url = `${this.apiUrl}/file`;

    // Create an object of formData
    const formData = new FormData();

    // Update the formData object
    formData.append('file', file, file.name);

    const req = new HttpRequest('POST', url, formData, {
      reportProgress: true,
    });

    // The `HttpClient.request` API produces a raw event stream
    // which includes start (sent), progress, and response events.
    // return this.http.request(req).pipe(
    //   map((event) => this.getEventMessage(event, file)),
    //   tap((message) => this.showProgress(message)),
    //   last(), // return last (completed) message to caller
    //   catchError(this.handleFileError(file))
    // );

    return this.http.post(url, formData, {
      reportProgress: true,
      observe: 'events'
    }).pipe(
      tap((_) => this.log('uploading transactions')),
      catchError(this.handleError('upload', []))
    );
  }

  /** Return distinct message for sent, upload progress, & response events */
  private getEventMessage(event: HttpEvent<any>, file: File) {
    switch (event.type) {
      case HttpEventType.Sent:
        return `Uploading file "${file.name}" of size ${file.size}.`;

      case HttpEventType.UploadProgress:
        // Compute and show the % done:
        const percentDone = Math.round(
          (100 * event.loaded) / (event.total ?? 0)
        );
        return `File "${file.name}" is ${percentDone}% uploaded.`;

      case HttpEventType.Response:
        return `File "${file.name}" was completely uploaded!`;

      default:
        return `File "${file.name}" surprising upload event: ${event.type}.`;
    }
  }

  /**
   * Returns a function that handles Http upload failures.
   * @param file - File object for file being uploaded
   *
   * When no `UploadInterceptor` and no server,
   * you'll end up here in the error handler.
   */
  private handleFileError(file: File) {
    const userMessage = `${file.name} upload failed.`;

    return (error: HttpErrorResponse) => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      const message =
        error.error instanceof Error
          ? error.error.message
          : `server returned code ${error.status} with body "${error.error}"`;

      this.messageService.add(`${userMessage} ${message}`);

      // Let app keep running but indicate failure.
      return of(userMessage);
    };
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
    this.messageService.add(`TransactionService: ${message}`);
  }

  private showProgress(message: string) {
    this.messageService.add(message);
  }
}
