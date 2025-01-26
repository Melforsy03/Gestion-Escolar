import { TestBed } from '@angular/core/testing';

import { StudentService } from '../../service/estudiante.service';

describe('Tables.ServiceService', () => {
  let service: StudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
