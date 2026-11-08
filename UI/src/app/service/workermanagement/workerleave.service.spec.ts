import { TestBed } from '@angular/core/testing';

import { WorkerleaveService } from './workerleave.service';

describe('WorkerleaveService', () => {
  let service: WorkerleaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkerleaveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
