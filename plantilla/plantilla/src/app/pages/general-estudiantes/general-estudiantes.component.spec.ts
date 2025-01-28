import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralEstudiantesComponent } from './general-estudiantes.component';

describe('GeneralEstudiantesComponent', () => {
  let component: GeneralEstudiantesComponent;
  let fixture: ComponentFixture<GeneralEstudiantesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GeneralEstudiantesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralEstudiantesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
