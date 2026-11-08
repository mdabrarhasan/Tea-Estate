import { TestBed } from '@angular/core/testing';

import { DailypluckingService } from './dailyplucking.service';

describe('DailypluckingService', () => {
  let service: DailypluckingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DailypluckingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
