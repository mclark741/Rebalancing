import { Component } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';

import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styles: [
    'input[type=file] { font-size: 1.2rem; margin-top: 1rem; display: block; }',
  ],
  providers: [TransactionService],
})
export class UploaderComponent {
  message = '';
  @Output() newItemEvent = new EventEmitter<string>();

  constructor(private transactionService: TransactionService) {}

  addNewItem(value: string) {
    this.newItemEvent.emit(value);
  }
  
  onPicked(input: HTMLInputElement) {
    const file = input.files?.[0];
    if (file) {
      // this.transactionService.upload(file).subscribe((msg) => {
      //   input.value = '';
      //   this.message = msg;
      // });
      this.transactionService.upload(file).subscribe((msg) => {
        input.value = '';
        this.addNewItem("upload complete");
        console.log(msg);
      });
    }
  }
}

/*
Copyright Google LLC. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at https://angular.io/license
*/
