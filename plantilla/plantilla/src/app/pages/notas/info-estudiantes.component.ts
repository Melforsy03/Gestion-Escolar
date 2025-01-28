import { Component, OnInit } from '@angular/core';
import { StudentGradingService } from 'src/app/service/generaStud.service';

@Component({
  selector: 'app-info-estudiantes',
  templateUrl: './info-estudiantes.component.html',
  styleUrls: ['./info-estudiantes.component.css']
})
export class InfoEstudiantesComponent implements OnInit {
  subjects: any[] = [];
  students: any[] = [];
  results: any[] = [];
  role : string ='';
  displayedStudents = [];
  globalSearchQuery = '';
  selectedSubject = '';
  editMode: boolean[] = [];
  viewMode: string = 'assign'; // Alterna entre 'assign' y 'list'
  notes: any[] = []; // Almacena las notas obtenidas de la API
  constructor(private gradingService: StudentGradingService) {}

  ngOnInit(): void {
    this.role = this.gradingService.getRole();
    this.getSubjects(this.role); // Replace 'currentUser' with the actual username logic
    
  }
  getSubjects(userName: string): void {
    this.gradingService.getSubjects(userName).subscribe(
      (response: any) => {
        this.subjects = response.subjects || []; // Asigna el array de subjects o un array vacío si no hay datos
        if (this.subjects.length > 0) {
          // Selecciona la primera asignatura por defecto
          this.selectedSubject = this.subjects[0].IdSub;
          this.getStudents(this.selectedSubject); // Pasa el idSub de la primera asignatura
        } else {
          this.selectedSubject = ''; // Deja en blanco si no hay asignaturas
        }
      },
      (error) => console.error('Error fetching subjects:', error)
    );
  }
  

  getStudents(subjectId: string): void {
    console.log(subjectId);
    this.gradingService.getStudents(+subjectId).subscribe(
      (response: any) => {
        console.log('Respuesta del servidor para estudiantes:', response);
  
        // Manejo flexible de la respuesta
        if (Array.isArray(response)) {
          this.students = response;
        } else if (response.students && Array.isArray(response.students)) {
          this.students = response.students;
        } else {
          console.error('La respuesta no contiene un array válido:', response);
          this.students = [];
        }
  
        this.updateDisplayedStudents();
      },
      (error) => console.error('Error fetching students:', error)
    );
  }
  
  updateDisplayedStudents(): void {
    if (!Array.isArray(this.students)) {
      console.error('this.students no es un array:', this.students);
      return;
    }
  
    this.displayedStudents = this.students.filter((student) => {
      const matchesSearch =
        !this.globalSearchQuery ||
        student.nameStud.toLowerCase().includes(this.globalSearchQuery.toLowerCase());
      return matchesSearch;
    });
  }
  assignGrade(student: any , index: number): void {
    const payload = {
      userName: this.role, // Reemplazar con la lógica de usuario actual
      idSub: this.selectedSubject,
      idStud: student.idStud,
      studentGrades: student.grade
    };

    this.gradingService.assignGrade(payload).subscribe(
      () => {
        console.log('Nota asignada correctamente:', payload);
  
        // Actualiza la tabla después de guardar
        this.displayedStudents[index].grade = student.grade;
  
        // Desactiva el modo de edición
        this.editMode[index] = false;
  
      },
      (error) => {
        console.error('Error asignando la nota:', error);
        alert('Ocurrió un error al asignar la nota');
      }
    );
  }
  onSubjectChange(subjectId: string): void {
    this.selectedSubject = subjectId;
    this.getStudents(subjectId);
  }

  toggleEditMode(index: number): void {
    this.editMode[index] = !this.editMode[index];
    if (!this.editMode[index]) {
      console.log('Guardando datos:', this.displayedStudents[index]);
    }
  }
  toggleViewMode(): void {
    this.viewMode = this.viewMode === 'assign' ? 'list' : 'assign';
    if (this.viewMode === 'list') {
      this.getNotes();
    }
  }

  getNotes(): void {
    const UserName = this.role;
    this.gradingService.getNotes(UserName).subscribe(
      (notes: any[]) => {
        this.notes = notes;
      },
      (error) => console.error('Error fetching notes:', error)
    );
  }
}
