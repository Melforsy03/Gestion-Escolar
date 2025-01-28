import { Component, OnInit } from '@angular/core';
import { CalificacionesService, Calificacion } from 'src/app/service/calificacionesPofesores.service';

@Component({
  selector: 'app-calificaciones',
  templateUrl: './calificacionesProfesores.component.html',
  styleUrls: ['./calificacionesProfesores.component.css'],
})
export class CalificacionesComponent implements OnInit {
  calificaciones: Calificacion[] = [];

  constructor(private calificacionesService: CalificacionesService) {}

  ngOnInit(): void {
    this.loadCalificaciones();
  }

  loadCalificaciones(): void {
    this.calificacionesService.listCalificaciones().subscribe(
      (response) => {
        this.calificaciones = response;
      },
      (error) => {
        console.error('Error al cargar las calificaciones:', error);
      }
    );
  }
}
