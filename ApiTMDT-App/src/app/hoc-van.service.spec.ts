import { TestBed } from '@angular/core/testing';

import { HocVanService } from './hoc-van.service';

describe('HocVanService', () => {
  let service: HocVanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HocVanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
