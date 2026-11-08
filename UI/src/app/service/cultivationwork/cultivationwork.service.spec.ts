import { TestBed } from '@angular/core/testing';

import { CultivationworkService } from './cultivationwork.service';

describe('CultivationworkService', () => {
  let service: CultivationworkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CultivationworkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
