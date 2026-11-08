import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GardenmanagementComponent } from './gardenmanagement.component';

describe('GardenmanagementComponent', () => {
  let component: GardenmanagementComponent;
  let fixture: ComponentFixture<GardenmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GardenmanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GardenmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
