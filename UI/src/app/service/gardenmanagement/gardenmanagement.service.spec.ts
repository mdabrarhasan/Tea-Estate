import { TestBed } from '@angular/core/testing';

import { GardenmanagementService } from './gardenmanagement.service';

describe('GardenmanagementService', () => {
  let service: GardenmanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GardenmanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
