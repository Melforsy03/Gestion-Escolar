import { Component, OnInit } from '@angular/core';
import { CalificacionesService } from 'src/app/service/otergar.service';
@Component({
  selector: 'app-evaluacion',
  templateUrl: './evaluacion.component.html',
  styleUrls: ['./evaluacion.component.scss']
})
export class EvaluacionComponent implements OnInit {
  calificaciones: any[] = [];
  userName: string = 'SuperAdmin'; // Reemplaza con la lógica de autenticación

  constructor(private calificacionesService: CalificacionesService) {}

  ngOnInit() {
    this.cargarCalificaciones();
  }

  cargarCalificaciones() {
    this.calificacionesService.getCalificaciones(this.userName).subscribe(
      (response) => {
        console.log('Datos recibidos:', response);
        this.calificaciones = response || [];
      },
      (error) => {
        console.error('Error al cargar las calificaciones:', error);
      }
    );
  }

  asignarNota(calificacion: any) {
    const nota = prompt(`Ingrese la nota para ${calificacion.subjectName}:`);
    if (nota !== null && !isNaN(Number(nota))) {
      const payload = {
        idProf: calificacion.idProf,
        idStud: calificacion.idStud,
        idSub: calificacion.idSub,
        idCourse: calificacion.idCourse,
        evaluation: Number(nota)
      };

      this.calificacionesService.asignarNota(payload).subscribe(
        () => {
          alert('✅ Nota asignada con éxito');
        },
        (error) => {
          console.error('❌ Error al asignar la nota:', error);
          alert('Error al asignar la nota');
        }
      );
    } else {
      alert('⚠ Debe ingresar un número válido');
    }
  }
}