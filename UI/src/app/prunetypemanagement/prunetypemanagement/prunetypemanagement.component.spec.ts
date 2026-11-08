import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrunetypemanagementComponent } from './prunetypemanagement.component';

describe('PrunetypemanagementComponent', () => {
  let component: PrunetypemanagementComponent;
  let fixture: ComponentFixture<PrunetypemanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PrunetypemanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrunetypemanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
