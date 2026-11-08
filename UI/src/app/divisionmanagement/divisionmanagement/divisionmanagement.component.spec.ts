import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DivisionmanagementComponent } from './divisionmanagement.component';

describe('DivisionmanagementComponent', () => {
  let component: DivisionmanagementComponent;
  let fixture: ComponentFixture<DivisionmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DivisionmanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DivisionmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
