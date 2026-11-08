import { TestBed } from '@angular/core/testing';

import { PluckingtaskallocationService } from './pluckingtaskallocation.service';

describe('PluckingtaskallocationService', () => {
  let service: PluckingtaskallocationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluckingtaskallocationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
