import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { transactionComponent } from './transaction.component';
import { CommonModule } from '@angular/common';

describe('transactionComponent', () => {
  let component: transactionComponent;
  let fixture: ComponentFixture<transactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [transactionComponent],
      imports: [ReactiveFormsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(transactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});













