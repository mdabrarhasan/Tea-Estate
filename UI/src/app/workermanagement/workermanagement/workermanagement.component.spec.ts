import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkermanagementComponent } from './workermanagement.component';

describe('WorkermanagementComponent', () => {
  let component: WorkermanagementComponent;
  let fixture: ComponentFixture<WorkermanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkermanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkermanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
