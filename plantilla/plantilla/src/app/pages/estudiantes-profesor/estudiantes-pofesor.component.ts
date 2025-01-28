

import { Component, OnInit } from '@angular/core';
import { EstudentService } from '../../service/estudiantes-profesor.service';


@Component({
  selector: 'app-estudiante',
  templateUrl: './estudiantes-pofesor.component.html',
  styleUrls: ['./estudiantes-pofesor.component.css'],
})
export class EstudiantesProfesor implements OnInit {
  students: Array<{ id: number; idc: number; student: { nameStud: string; age: number; eActivity: boolean } }> = [];

  constructor(private estudianteService: EstudentService) {}

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents() {
    this.estudianteService.getStudents().subscribe(
      (data) => {
        this.students = data;

        console.log(this.students);
      },
      (error) => {
        console.error('Error al cargar estudiantes:', error);
      }
    );
  }
}
