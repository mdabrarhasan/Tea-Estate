import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RationmanagementComponent } from './rationmanagement.component';

describe('RationmanagementComponent', () => {
  let component: RationmanagementComponent;
  let fixture: ComponentFixture<RationmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RationmanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RationmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
