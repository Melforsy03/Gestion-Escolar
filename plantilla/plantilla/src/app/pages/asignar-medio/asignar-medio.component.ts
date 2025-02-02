import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { SolicitudService } from 'src/app/service/asignar-medio.service';

@Component({
  selector: 'app-solicitud',
  templateUrl: './asignar-medio.component.html',
  styleUrls: ['./asignar-medio.component.css'],
})
export class AsignarComponent implements OnInit {
  solicitudForm: FormGroup;
  result: any[] = []; // Para almacenar los resultados del endpoint de aulas
  mediosDisponibles: any[] = []; // Medios tecnológicos disponibles para un aula seleccionada
  aulas: any[] = []; // Aulas disponibles
  selectedClassroom: any = null; // Aula seleccionada
  selectedTechnologicalMeans: any[] = []; // Medios tecnológicos seleccionados

  constructor(private fb: FormBuilder, private http: HttpClient, private solicitud: SolicitudService) {
    this.solicitudForm = this.fb.group({
      aula: [''],
      medios: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.cargarAulas();
  }

  // Cargar todas las aulas
  cargarAulas() {
    this.solicitud.getAvailableClassrooms().subscribe(
      (response) => {
        if (response) {
          this.aulas = response;
        } else {
          console.error('No se pudieron obtener las aulas');
        }
      },
      (error) => {
        console.error('Error al cargar aulas:', error);
      }
    );
  }

  // Cuando seleccionamos un aula, obtener los medios tecnológicos disponibles para esa aula
  seleccionarAula(aula: any) {
    this.selectedClassroom = aula;
    this.solicitud.getTechnologicalMeansForClassroom(aula.idClassR).subscribe(
      (response) => {
        this.mediosDisponibles = response;
      },
      (error) => {
        console.error('Error al obtener los medios tecnológicos:', error);
      }
    );
  }

  // Seleccionar un medio tecnológico y asociarlo al aula
  asignarMedio(medioId: number) {
    if (!this.selectedClassroom) {
      alert('Debe seleccionar un aula antes de asignar un medio');
      return;
    }

    // Asegurarse de que los datos se envían correctamente en el formato requerido
    const data = {
      idClassRoom: this.selectedClassroom.idClassR, // ID del aula seleccionada
      idTechMean: medioId, // ID del medio seleccionado
    };

    console.log('Datos enviados al backend:', data); // Log para verificar lo que se envía

    // Enviar la solicitud con el ID del aula y del medio al backend
    this.solicitud.assignTechnologicalMeanToClassroom(data).subscribe(
      (response) => {
        console.log('Medio asignado exitosamente', response);
        // Agregar el medio a la lista de medios asignados
        const medioAsignado = this.mediosDisponibles.find(m => m.id === medioId);
        if (medioAsignado) {
          this.selectedTechnologicalMeans.push(medioAsignado);
        }
      },
      (error) => {
        console.error('Error al asignar el medio tecnológico:', error);
      }
    );
  }

  // Volver a la lista de aulas
  volverAulas() {
    this.selectedClassroom = null;
    this.mediosDisponibles = [];
  }
}
