import { TestBed } from '@angular/core/testing';

import { LeavemanagementService } from './leavemanagement.service';

describe('LeavemanagementService', () => {
  let service: LeavemanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeavemanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
