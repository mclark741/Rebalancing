import { HttpEventType, HttpProgressEvent } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { TransactionService } from '../transaction.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-pretty-uploader',
  templateUrl: './pretty-uploader.component.html',
  styleUrls: ['./pretty-uploader.component.scss'],
})
export class PrettyUploaderComponent implements OnInit {
  @Input()
  requiredFileType: string = '';

  @Output() uploadCompleteEvent = new EventEmitter<string>();
  
  fileName = '';
  uploadProgress!: number;
  uploadSub!: Subscription;

  constructor(private transactionService: TransactionService) {}

  ngOnInit(): void {}

  onFileDropped(file: FileList) {
    if (file && file.length) {
      this.uploadFile(file[0]);
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files?.length) {
      const file: File = input.files[0];
      if (file) {
        this.uploadFile(file);
      }
    }
  }

  uploadFile(file: File) {
    this.fileName = file.name;

    this.uploadSub = this.transactionService
      .upload(file)
      .pipe(finalize(() => this.reset()))
      .subscribe((event) => {
        const e = event as HttpProgressEvent;

        if (e && e.total) {
          console.log(e);
          if (e.type == HttpEventType.UploadProgress) {
            this.uploadProgress = Math.round(100 * (e.loaded / e.total));
          }
        }
      });
  }

  reset() {
    this.uploadProgress = 0;
    this.uploadSub = new Subscription();
    this.uploadCompleteEvent.emit('upload complete');
  }
}
