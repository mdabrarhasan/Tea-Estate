import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyattendancedetailsComponent } from './dailyattendancedetails.component';

describe('DailyattendancedetailsComponent', () => {
  let component: DailyattendancedetailsComponent;
  let fixture: ComponentFixture<DailyattendancedetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DailyattendancedetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyattendancedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
