import { Component, OnInit } from '@angular/core';
import { EstudentService } from '../../service/estudiante.service';

interface Student {
  id: number;
  idc: number;
  student: {
    nameStud: string;
    age: number;
    eActivity: boolean;
  };
}

@Component({
  selector: 'app-estudiante',
  templateUrl: './estudiantes-pofesor.component.html',
  styleUrls: ['./estudiantes-pofesor.component.css'],
})
export class EstudiantesProfesor implements OnInit {
  students: Student[] = [];
  errorMessage: string = ''; // Para manejar errores

  constructor(private estudianteService: EstudentService) {}

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents() {
    this.estudianteService.getStudents().subscribe(
      (data) => {
        this.students = data;
        console.log(this.students); // Verificar la carga de estudiantes
      },
      (error) => {
        this.errorMessage = 'Error al cargar estudiantes. Por favor, intente nuevamente.';
        console.error('Error al cargar estudiantes:', error);
      }
    );
  }
}
