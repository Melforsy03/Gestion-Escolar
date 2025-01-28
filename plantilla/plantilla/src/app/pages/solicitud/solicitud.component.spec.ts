import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { SolicitudComponent } from './solicitud.component';

describe('SolicitudComponent', () => {
  let component: SolicitudComponent;
  let fixture: ComponentFixture<SolicitudComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SolicitudComponent],
      imports: [FormsModule, ReactiveFormsModule],
    }).compileComponents();

    fixture = TestBed.createComponent(SolicitudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with default values', () => {
    expect(component.solicitudForm).toBeTruthy();
    const aulaControl = component.solicitudForm.get('aula');
    expect(aulaControl?.value).toBe('');
    expect(component.medios.controls.length).toBe(0);
  });

  it('should add a new medio', () => {
    component.addMedio();
    expect(component.medios.controls.length).toBe(1);
    expect(component.medios.controls[0].value).toEqual({ nombre: '', cantidad: '' });
  });

  it('should remove a medio', () => {
    component.addMedio();
    component.addMedio();
    expect(component.medios.controls.length).toBe(2);

    component.removeMedio(0);
    expect(component.medios.controls.length).toBe(1);
  });

  it('should handle form submission', () => {
    const mockFormValue = {
      aula: 'Aula 101',
      medios: [
        { nombre: 'proyector', cantidad: '2' },
        { nombre: 'sillas', cantidad: '30' },
      ],
    };
    spyOn(component, 'CargarInfo');

    component.solicitudForm.patchValue(mockFormValue);
    fixture.debugElement.query(By.css('form')).triggerEventHandler('ngSubmit', null);

    expect(component.CargarInfo).toHaveBeenCalled();
  });

  it('should display results when result is available', () => {
    component.result = {
      dictionary: {
        Matemáticas: [
          ['Proyector', 1],
          ['Pizarra', 1],
        ],
      },
      availableRooms: [101, 102],
    };
    fixture.detectChanges();

    const resultSection = fixture.debugElement.query(By.css('.result-container'));
    expect(resultSection).toBeTruthy();

    const rows = fixture.debugElement.queryAll(By.css('.table tbody tr'));
    expect(rows.length).toBeGreaterThan(0);

    const aulasList = fixture.debugElement.queryAll(By.css('ul li'));
    expect(aulasList.length).toBe(2);
  });

  it('should not display results when result is null', () => {
    component.result = null;
    fixture.detectChanges();

    const resultSection = fixture.debugElement.query(By.css('.result-container'));
    expect(resultSection).toBeFalsy();
  });
});