import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { SecurityComponent } from './security/security.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { MatSliderModule } from '@angular/material/slider';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { NavigationComponent } from './navigation/navigation.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PortfolioTableComponent } from './portfolio-table/portfolio-table.component';
import { TransactionTableComponent } from './transaction-table/transaction-table.component';
import { UploaderComponent } from './uploader/uploader.component';
import { MessagesComponent } from './messages/messages.component';
import { TransactionExchangeTableComponent } from './transaction-exchange-table/transaction-exchange-table.component';
import { AppConfig } from './app-config';
import { PrettyUploaderComponent } from './pretty-uploader/pretty-uploader.component';
import { DragDropFileUploadDirective } from './drag-drop-file-upload.directive';

export function initializeApp(appConfig: AppConfig) {
  return () => appConfig.load();
}

@NgModule({
  declarations: [
    AppComponent,
    TransactionsComponent,
    PortfolioComponent,
    SecurityComponent,
    NavigationComponent,
    PortfolioTableComponent,
    TransactionTableComponent,
    UploaderComponent,
    MessagesComponent,
    TransactionExchangeTableComponent,
    PrettyUploaderComponent,
    DragDropFileUploadDirective,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatSliderModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
  ],
  providers: [
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfig],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
