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
  aulas: any[] = [];
  mediosDisponibles: any[] = [];
  selectedClassroom: any = null;
  selectedTechnologicalMeans: any[] = [];
  assignedTechMeans: any[] = [];
  showAssignedMeans = false;

  constructor(private fb: FormBuilder, private http: HttpClient, private solicitud: SolicitudService) {
    this.solicitudForm = this.fb.group({
      aula: [''],
      medios: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.cargarAulas();
  }

  cargarAulas() {
    this.solicitud.getAvailableClassrooms().subscribe(
      (response) => {
        this.aulas = response || [];
      },
      (error) => {
        console.error('Error al cargar aulas:', error);
      }
    );
  }

  seleccionarAula(aula: any) {
    this.selectedClassroom = aula;
    this.solicitud.getTechnologicalMeansForClassroom(aula.idClassR).subscribe(
      (response) => {
        this.mediosDisponibles = response || [];
      },
      (error) => {
        console.error('Error al obtener medios tecnológicos:', error);
      }
    );
  }

  asignarMedio(medioId: number) {
    if (!this.selectedClassroom) {
      alert('Debe seleccionar un aula antes de asignar un medio');
      return;
    }

    const data = {
      idClassRoom: this.selectedClassroom.idClassR,
      idTechMean: medioId,
    };

    this.solicitud.assignTechnologicalMeanToClassroom(data).subscribe(
      (response) => {
        console.log('Medio asignado exitosamente', response);
        const medioAsignado = this.mediosDisponibles.find(m => m.idMean === medioId);
        if (medioAsignado) {
          this.selectedTechnologicalMeans.push(medioAsignado);
        }
      },
      (error) => {
        console.error('Error al asignar el medio:', error);
      }
    );
  }

  cargarMediosAsignados() {
    this.solicitud.getAssignedTechMeans().subscribe(
      (response) => {
        this.assignedTechMeans = response || [];
      },
      (error) => {
        console.error('Error al cargar medios asignados:', error);
      }
    );
  }

  eliminarAsignacion(idClassRoomTech: number) {
    this.solicitud.deleteAssignedTechMean(idClassRoomTech).subscribe(
      (response) => {
        console.log('Asignación eliminada exitosamente', response);
        this.assignedTechMeans = this.assignedTechMeans.filter(m => m.idClassRoomTech !== idClassRoomTech);
      },
      (error) => {
        console.error('Error al eliminar asignación:', error);
      }
    );
  }

  toggleAssignedMeans() {
    this.showAssignedMeans = !this.showAssignedMeans;
    this.selectedClassroom = null;
    if (this.showAssignedMeans) {
      this.cargarMediosAsignados();
    }
  }

  volverAulas() {
    this.selectedClassroom = null;
    this.showAssignedMeans = false;
    this.mediosDisponibles = [];
  }
}
