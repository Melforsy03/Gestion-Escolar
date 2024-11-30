import { Component, OnInit } from "@angular/core";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-notas",
  templateUrl: "notas.component.html",
  styleUrls : ["./notas.component.css"]
})
export class NotasComponent implements OnInit {

  subjects = ['Matemáticas', 'Ciencias', 'Historia']; // Opciones de asignaturas
  courses = ['Primero', 'Segundo', 'Tercero' ,'Cuarto']; // Opciones de cursos

  selectedSubject: string = ''; // Asignatura seleccionada
  selectedCourse: string = ''; // Curso seleccionado

  students: any[] = [
    { name: 'Carlos Pérez', subject: 'Matemáticas', course: 'Primero', attendance: 90, average: 8.5 },
    { name: 'Ana García', subject: 'Matemáticas', course: 'Segundo', attendance: 85, average: 9.0 },
    { name: 'Luis Martínez', subject: 'Ciencias', course: 'Primero', attendance: 95, average: 7.5 },
    { name: 'María López', subject: 'Ciencias', course: 'Segundo', attendance: 80, average: 6.8 },
    { name: 'Sofía Rodríguez', subject: 'Historia', course: 'Tercero', attendance: 88, average: 7.2 },
    { name: 'Pedro González', subject: 'Historia', course: 'Cuarto', attendance: 70, average: 6.0 },
    { name: 'Lucía Fernández', subject: 'Inglés', course: 'Primero', attendance: 92, average: 8.9 },
    { name: 'Javier Ramírez', subject: 'Inglés', course: 'Segundo', attendance: 78, average: 7.6 },
  ];
  filteredStudents = [...this.students]; // Estudiantes filtrados por asignatura y curso
  displayedStudents = [...this.students];
  globalSearchQuery: string = '';
  editMode: boolean[] = [];
  constructor() {
    
    // Inicializa el estado de edición como falso para todos los estudiantes
    this.editMode = this.students.map(() => false);
  }

  ngOnInit() {
    // Establecer la primera asignatura como valor por defecto
    if (this.subjects.length > 0) {
      this.selectedSubject = this.subjects[0];
    }

    // Realizar un filtrado inicial
    this.filterStudents();
  }

  onFilterChange() {
    this.filterStudents();
  }

  filterStudents() {
    // Lógica para filtrar estudiantes según la asignatura y curso
    this.filteredStudents = this.students.filter(
      (student) =>
        (this.selectedSubject ? student.subject === this.selectedSubject : true) &&
        (this.selectedCourse ? student.course === this.selectedCourse : true)
    );
  }

  searchQuery: string = '';

  updateDisplayedStudents(): void {
    const query = this.globalSearchQuery.trim().toLowerCase();

    this.displayedStudents = this.students.filter((student) => {
      // Filtrado por búsqueda global
      const matchesSearch = !query || student.name.toLowerCase().includes(query);

      // Filtrado por asignatura y curso
      const subjectMatch =
        !this.selectedSubject || this.selectedSubject === student.subject; // Lógica de asignatura
      const courseMatch =
        !this.selectedCourse || this.selectedCourse === student.course; // Lógica de curso

      return matchesSearch && subjectMatch && courseMatch;
    });
  }

/**
 * Llama al actualizar la búsqueda global.
 */
onGlobalSearch(): void {
  this.updateDisplayedStudents();
}
  editStudent(index: number): void {
    const student = this.filteredStudents[index];
    console.log('Editar estudiante:', student);
    // Lógica para editar estudiante
  }


   toggleEditMode(index: number): void {
     // Cambia el estado de edición para el estudiante específico
     this.editMode[index] = !this.editMode[index];
     if (!this.editMode[index]) {
       // Aquí puedes guardar los cambios en un backend, si es necesario
       console.log('Datos guardados:', this.students[index]);
     }
   }

}
