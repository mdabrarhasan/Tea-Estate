import { TestBed } from '@angular/core/testing';

import { ShiftmanagementService } from './shiftmanagement.service';

describe('ShiftmanagementService', () => {
  let service: ShiftmanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShiftmanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
