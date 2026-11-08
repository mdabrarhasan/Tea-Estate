import { TestBed } from '@angular/core/testing';

import { WorkertypeService } from './workertype.service';

describe('WorkertypeService', () => {
  let service: WorkertypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkertypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
