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
    console.log('Componente cargado');
    // Inicialmente podríamos cargar las calificaciones al cargar el componente si se quiere
    // this.loadCalificaciones();  // Puedes descomentar esto si deseas cargar automáticamente las calificaciones al inicio
  }

  // Método para cargar las calificaciones
  loadCalificaciones(): void {
    console.log('Cargando las calificaciones...');
    this.calificacionesService.listCalificaciones().subscribe(
      (response) => {
        console.log('Datos recibidos del backend:', response);
        this.calificaciones = response;
        console.log('Calificaciones asignadas:', this.calificaciones);
      },
      (error) => {
        console.error('Error al cargar las calificaciones:', error);
        alert('Error al cargar las calificaciones. Por favor, inténtalo de nuevo.');
      }
    );
  }
}
