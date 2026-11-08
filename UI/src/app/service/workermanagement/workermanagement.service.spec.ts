import { TestBed } from '@angular/core/testing';

import { WorkermanagementService } from './workermanagement.service';

describe('WorkermanagementService', () => {
  let service: WorkermanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkermanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
