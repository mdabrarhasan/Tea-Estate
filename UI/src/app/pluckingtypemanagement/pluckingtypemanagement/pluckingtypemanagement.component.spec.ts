import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluckingtypemanagementComponent } from './pluckingtypemanagement.component';

describe('PluckingtypemanagementComponent', () => {
  let component: PluckingtypemanagementComponent;
  let fixture: ComponentFixture<PluckingtypemanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PluckingtypemanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PluckingtypemanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
