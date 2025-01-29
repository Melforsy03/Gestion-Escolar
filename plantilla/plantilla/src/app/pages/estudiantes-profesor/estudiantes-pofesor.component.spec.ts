import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EstudiantesProfesor } from './estudiantes-pofesor.component';
import { EstudentService } from '../../service/estudiante.service';
import { of, throwError } from 'rxjs';

describe('EstudiantesProfesor', () => {
  let component: EstudiantesProfesor;
  let fixture: ComponentFixture<EstudiantesProfesor>;
  let estudianteServiceMock: jasmine.SpyObj<EstudentService>;

  beforeEach(() => {
    // Creamos un espía del servicio EstudentService
    const spy = jasmine.createSpyObj('EstudentService', ['getStudents']);

    TestBed.configureTestingModule({
      declarations: [EstudiantesProfesor],
      providers: [
        { provide: EstudentService, useValue: spy } // Usamos el espía
      ]
    });

    fixture = TestBed.createComponent(EstudiantesProfesor);
    component = fixture.componentInstance;
    estudianteServiceMock = TestBed.inject(EstudentService) as jasmine.SpyObj<EstudentService>;

    // Inicializamos las variables para cada prueba
    component.errorMessage = '';
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load students successfully', () => {
    const mockStudents = [
      { id: 1, idc: 101, student: { nameStud: 'Juan', age: 20, eActivity: true } },
      { id: 2, idc: 102, student: { nameStud: 'Ana', age: 22, eActivity: false } },
    ];

    // Simulamos que el servicio devuelve los estudiantes
    estudianteServiceMock.getStudents.and.returnValue(of(mockStudents));

    component.loadStudents();

    fixture.detectChanges();

    // Verificamos que los estudiantes se hayan cargado correctamente
    expect(component.students).toEqual(mockStudents);
    expect(component.errorMessage).toBe('');
  });

  it('should handle error when loading students', () => {
    const errorResponse = new Error('Error al cargar estudiantes');

    // Simulamos que el servicio lanza un error
    estudianteServiceMock.getStudents.and.returnValue(throwError(() => errorResponse));

    component.loadStudents();

    fixture.detectChanges();

    // Verificamos que el mensaje de error sea actualizado
    expect(component.students).toEqual([]);
    expect(component.errorMessage).toBe('Error al cargar estudiantes. Por favor, intente nuevamente.');
  });
});
