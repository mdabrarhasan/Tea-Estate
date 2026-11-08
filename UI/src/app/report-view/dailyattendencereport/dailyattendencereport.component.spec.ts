import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyattendencereportComponent } from './dailyattendencereport.component';

describe('DailyattendencereportComponent', () => {
  let component: DailyattendencereportComponent;
  let fixture: ComponentFixture<DailyattendencereportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DailyattendencereportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailyattendencereportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
