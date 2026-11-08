import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceregistrationComponent } from './deviceregistration.component';

describe('DeviceregistrationComponent', () => {
  let component: DeviceregistrationComponent;
  let fixture: ComponentFixture<DeviceregistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeviceregistrationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeviceregistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
