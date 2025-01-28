import { Component, OnInit } from '@angular/core';
import { EstudentService } from '../../service/estudiantes-profesor.service';

@Component({
  selector: 'app-estudiante',
  templateUrl: './estudiantes-pofesor.component.html',
  styleUrls: ['./estudiantes-pofesor.component.css'],
})
export class EstudiantesProfesor implements OnInit {
  students: Array<{ id: number; idc: number; student: { nameStud: string; age: number; eActivity: boolean } }> = [];
  isAddStudentModalOpen = false;

  constructor(private estudianteService: EstudentService) {}

  ngOnInit() {
    // No cargar estudiantes automáticamente, solo cuando se presiona el botón "Listar"
  }

  // Cargar estudiantes filtrados por nombre de usuario
  loadStudents() {
    const username = localStorage.getItem('username');  // Obtener el nombre de usuario desde localStorage
    if (username) {
      this.estudianteService.getStudentsByUser(username).subscribe(
        (data) => {
          console.log('Estudiantes recibidos:', data);
          this.students = data;
        },
        (error) => {
          console.error('Error al cargar estudiantes:', error);
        }
      );
    } else {
      console.error('Usuario no encontrado en localStorage');
    }
  }
}
