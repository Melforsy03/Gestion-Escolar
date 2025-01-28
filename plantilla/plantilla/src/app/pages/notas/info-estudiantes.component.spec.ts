import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { NotasComponent } from './info-estudiantes.component';

describe('InfoEstudiantesComponent', () => {
  let component: NotasComponent;
  let fixture: ComponentFixture<NotasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NotasComponent],
      imports: [FormsModule],
    }).compileComponents();

    fixture = TestBed.createComponent(NotasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with no students displayed', () => {
    expect(component.displayedStudents).toEqual([]);
  });

  it('should filter students by global search query', () => {
    component.displayedStudents = [
      { name: 'Juan', attendance: '90%', average: 8.5 },
      { name: 'Ana', attendance: '95%', average: 9.0 },
    ];
    component.globalSearchQuery = 'Ana';
    component.updateDisplayedStudents();

    expect(component.displayedStudents.length).toBe(1);
    expect(component.displayedStudents[0].name).toBe('Ana');
  });

  it('should filter students by subject', () => {
    component.subjects = ['Math', 'Science'];
    component.displayedStudents = [
      { name: 'Juan', subject: 'Math', attendance: '90%', average: 8.5 },
      { name: 'Ana', subject: 'Science', attendance: '95%', average: 9.0 },
    ];
    component.selectedSubject = 'Science';
    component.updateDisplayedStudents();

    expect(component.displayedStudents.length).toBe(1);
    expect(component.displayedStudents[0].subject).toBe('Science');
  });

  it('should toggle edit mode and save changes', () => {
    component.displayedStudents = [
      { name: 'Juan', attendance: '90%', average: 8.5 },
    ];
    component.editMode = [false];

    component.toggleEditMode(0);
    expect(component.editMode[0]).toBeTrue();

    component.displayedStudents[0].attendance = '95%';
    component.toggleEditMode(0);
    expect(component.editMode[0]).toBeFalse();
    expect(component.displayedStudents[0].attendance).toBe('95%');
  });

  it('should show message when no students are displayed', () => {
    component.displayedStudents = [];
    fixture.detectChanges();

    const message = fixture.debugElement.query(By.css('p')).nativeElement.textContent;
    expect(message).toContain('No se encontraron estudiantes.');
  });

  it('should display students in the table', () => {
    component.displayedStudents = [
      { name: 'Juan', attendance: '90%', average: 8.5 },
      { name: 'Ana', attendance: '95%', average: 9.0 },
    ];
    fixture.detectChanges();

    const rows = fixture.debugElement.queryAll(By.css('tbody tr'));
    expect(rows.length).toBe(2);
    expect(rows[0].query(By.css('td')).nativeElement.textContent).toContain('Juan');
  });
});
