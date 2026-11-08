import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GardenweatherComponent } from './gardenweather.component';

describe('GardenweatherComponent', () => {
  let component: GardenweatherComponent;
  let fixture: ComponentFixture<GardenweatherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GardenweatherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GardenweatherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
