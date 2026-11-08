import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendenceDailyReportComponent } from './attendence-daily-report.component';

describe('AttendenceDailyReportComponent', () => {
  let component: AttendenceDailyReportComponent;
  let fixture: ComponentFixture<AttendenceDailyReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AttendenceDailyReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendenceDailyReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
