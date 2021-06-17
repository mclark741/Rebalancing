import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionExchangeTableComponent } from './transaction-exchange-table.component';

describe('TransactionExchangeTableComponent', () => {
  let component: TransactionExchangeTableComponent;
  let fixture: ComponentFixture<TransactionExchangeTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransactionExchangeTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionExchangeTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
