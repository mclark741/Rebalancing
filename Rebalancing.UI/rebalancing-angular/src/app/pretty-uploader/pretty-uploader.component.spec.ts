import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrettyUploaderComponent } from './pretty-uploader.component';

describe('PrettyUploaderComponent', () => {
  let component: PrettyUploaderComponent;
  let fixture: ComponentFixture<PrettyUploaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrettyUploaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrettyUploaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
