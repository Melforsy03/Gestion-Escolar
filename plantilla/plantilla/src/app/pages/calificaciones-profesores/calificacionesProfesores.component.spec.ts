import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CalificacionesComponent } from './calificacionesProfesores.component';
import { CalificacionesService } from 'src/app/service/calificacionesPofesores.service';
import { of, throwError } from 'rxjs';

// Mock del servicio CalificacionesService
class MockCalificacionesService {
  listCalificaciones() {
    return of([
      { id: 1, profesor: 'Profesor A', calificacion: 90 },
      { id: 2, profesor: 'Profesor B', calificacion: 85 }
    ]);
  }
}

describe('CalificacionesComponent', () => {
  let component: CalificacionesComponent;
  let fixture: ComponentFixture<CalificacionesComponent>;
  let mockCalificacionesService: MockCalificacionesService;

  beforeEach(() => {
    mockCalificacionesService = new MockCalificacionesService();

    TestBed.configureTestingModule({
      declarations: [CalificacionesComponent],
      providers: [
        { provide: CalificacionesService, useValue: mockCalificacionesService }
      ]
    });

    fixture = TestBed.createComponent(CalificacionesComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load calificaciones on init', () => {
    component.ngOnInit();
    fixture.detectChanges();
    expect(component.calificaciones.length).toBe(2); // Esperamos que haya 2 calificaciones
    expect(component.calificaciones[0].profesor).toBe('Profesor A');
  });

  it('should handle errors when loading calificaciones', () => {
    const errorResponse = 'Error al cargar las calificaciones';
    spyOn(mockCalificacionesService, 'listCalificaciones').and.returnValue(throwError(() => new Error(errorResponse)));
    spyOn(window, 'alert'); // Mock alert

    component.loadCalificaciones();

    expect(window.alert).toHaveBeenCalledWith('Error al cargar las calificaciones. Por favor, inténtalo de nuevo.');
  });
});
