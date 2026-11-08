import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalarygenerateComponent } from './salarygenerate.component';

describe('SalarygenerateComponent', () => {
  let component: SalarygenerateComponent;
  let fixture: ComponentFixture<SalarygenerateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SalarygenerateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalarygenerateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
