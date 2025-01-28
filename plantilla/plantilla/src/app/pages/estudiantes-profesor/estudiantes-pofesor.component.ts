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
    // No cargamos estudiantes automáticamente
  }

  // Método para cargar estudiantes al presionar el botón "Listar"
  loadStudents() {
    this.estudianteService.getStudents().subscribe(
      (data) => {
        console.log('Estudiantes recibidos:', data);
        this.students = data;
      },
      (error) => {
        console.error('Error al cargar estudiantes:', error);
      }
    );
  }
}
