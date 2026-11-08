import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkerwiseattendancedetailsComponent } from './workerwiseattendancedetails.component';

describe('WorkerwiseattendancedetailsComponent', () => {
  let component: WorkerwiseattendancedetailsComponent;
  let fixture: ComponentFixture<WorkerwiseattendancedetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkerwiseattendancedetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkerwiseattendancedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
