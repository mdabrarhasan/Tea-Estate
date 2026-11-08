import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeavesummaryComponent } from './leavesummary.component';

describe('LeavesummaryComponent', () => {
  let component: LeavesummaryComponent;
  let fixture: ComponentFixture<LeavesummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeavesummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeavesummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
