import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstudiantesProfesor } from './estudiantes-pofesor.component';

describe('EstudiantesPofesorComponent', () => {
  let component: EstudiantesProfesor;
  let fixture: ComponentFixture<EstudiantesProfesor>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EstudiantesProfesor ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EstudiantesProfesor);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
