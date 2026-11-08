import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerwiseattendancesummaryComponent } from './workerwiseattendancesummary.component';

describe('WorkerwiseattendancesummaryComponent', () => {
  let component: WorkerwiseattendancesummaryComponent;
  let fixture: ComponentFixture<WorkerwiseattendancesummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkerwiseattendancesummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkerwiseattendancesummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
