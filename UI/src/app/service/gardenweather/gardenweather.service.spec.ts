import { TestBed } from '@angular/core/testing';

import { GardenweatherService } from './gardenweather.service';

describe('GardenweatherService', () => {
  let service: GardenweatherService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GardenweatherService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
