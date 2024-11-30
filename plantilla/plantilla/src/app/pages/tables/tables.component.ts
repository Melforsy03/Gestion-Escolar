import { Component, OnInit } from "@angular/core";
import { StudentService } from "./tables.service";
import { ChangeDetectorRef } from "@angular/core";
@Component({
  selector: "app-tables",
  templateUrl: "tables.component.html",
  styleUrls: ["./tables.component.css"]
  
})
export class TablesComponent implements OnInit {
  students: any[] = [];
  expandedIndexes: boolean[] = [];
  newStudent = { name: '', age: null, course: '', activities: '' };
  isAddStudentModalOpen = false;
  showConfirmModal = false;
  studentToDelete: any = null;
  filteredStudents = [];  // Estudiantes filtrados
  courses = ['Primero' ,'Segundo'];  // Lista de cursos disponibles
  searchQuery = '';  // Término de búsqueda
  selectedCourse = '';  // Curso seleccionado
  displayedStudents = [...this.students];
  globalSearchQuery: string = '';
  constructor(private studentService: StudentService ,private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.studentService.getStudents().subscribe((data) => {
      this.students = data;
      this.updateDisplayedStudents(); 

    });
  }
  updateDisplayedStudents() {
    this.filteredStudents = this.students.filter((student) => {
      const matchesCourse =
        !this.selectedCourse || student.course === this.selectedCourse;
      const matchesSearch =
        student.name
          .toLowerCase()
          .includes(this.globalSearchQuery.toLowerCase());
      return matchesCourse && matchesSearch;
    });
  }
  filterStudents() {
    this.filteredStudents = this.students.filter(student => {
      const matchesName = student.name.toLowerCase().includes(this.searchQuery.toLowerCase());
      const matchesCourse = this.selectedCourse ? student.course === this.selectedCourse : true;
      return matchesName && matchesCourse;
    });
  }
  toggleSubjects(index: number): void {
    console.log('Before toggle:', this.expandedIndexes[index]);
    this.expandedIndexes[index] = !this.expandedIndexes[index];
    console.log('Índice expandido:', index, 'Estado:', this.expandedIndexes[index]);
  
  }
    // Función para confirmar la eliminación del estudiante
    confirmDelete(student: any, index: number) {
      this.studentToDelete = student;
      this.showConfirmModal = true;
    }
    // Función para eliminar el estudiante
  // Función para eliminar el estudiante
  deleteStudent() {
    if (this.studentToDelete) {
      const index = this.students.indexOf(this.studentToDelete);
      if (index > -1) {
        this.students.splice(index, 1);
      }
    }
    this.closeConfirmModal(); // Cierra el modal después de eliminar
  }

  // Función para cerrar el modal de confirmación
  closeConfirmModal() {
    this.showConfirmModal = false;
    this.studentToDelete = null;
  }
    // Abrir modal para agregar estudiante
    openAddStudentModal() {
      this.isAddStudentModalOpen = true;
    }
  
    // Cerrar modal
    closeAddStudentModal() {
      this.isAddStudentModalOpen = false;
    }
  
    // Agregar nuevo estudiante
    addStudent() {
      this.students.push({ ...this.newStudent });
      this.closeAddStudentModal();
      this.newStudent = { name: '', age: null, course: '', activities: '' };  // Resetear los campos
    }
}
