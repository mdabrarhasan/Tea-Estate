import { TestBed } from '@angular/core/testing';

import { NonpluckingactivitiesService } from './nonpluckingactivities.service';

describe('NonpluckingactivitiesService', () => {
  let service: NonpluckingactivitiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NonpluckingactivitiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
