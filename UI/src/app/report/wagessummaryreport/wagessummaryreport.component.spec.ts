import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WagessummaryreportComponent } from './wagessummaryreport.component';

describe('WagessummaryreportComponent', () => {
  let component: WagessummaryreportComponent;
  let fixture: ComponentFixture<WagessummaryreportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WagessummaryreportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WagessummaryreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
