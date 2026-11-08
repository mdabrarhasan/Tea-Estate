import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { masterinformationComponent } from './masterinformation.component';
import { CommonModule } from '@angular/common';

describe('masterinformationComponent', () => {
  let component: masterinformationComponent;
  let fixture: ComponentFixture<masterinformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [masterinformationComponent],
      imports: [ReactiveFormsModule]
    }).compileComponents();

    fixture = TestBed.createComponent(masterinformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});













