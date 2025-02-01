import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { SolicitudService } from 'src/app/service/solicitud.service';
@Component({
  selector: 'app-solicitud',
  templateUrl: './solicitud.component.html',
  styleUrls: ['./solicitud.component.css'],

})
export class SolicitudComponent implements OnInit {
  solicitudForm: FormGroup;
  result: any [] = []; // Para almacenar los resultados del endpoint
  userName: string = '';
  selectedSubject: string = ''; // Asignatura seleccionada
  selectedMedios: any[] = []; // Medios para la asignatura seleccionada
  isModalVisible: boolean = false;
  medioSeleccionado: any = [];
  cantidadReserva: number = 1;
  reserveMeans: any[] = [];
  constructor(private fb: FormBuilder, private http: HttpClient , private solicitud :SolicitudService) {
    this.solicitudForm = this.fb.group({
      aula: [''],
      medios: this.fb.array([]),
    });
  }

ngOnInit(): void {
    this.CargarInfo();
}

CargarInfo() {
  this.userName = this.solicitud.getUser();
  this.solicitud.checkAvailableClassroomsAndMeans(this.userName).subscribe(
    (response) => {
      if (response && response.data) {
        // Transformar los datos de la API
        this.result = Object.entries(response.data).map(([subject, medios]) => ({
          subject,
          medios: Object.entries(medios).map(([medio, cantidad]) => ({
            medio,
            cantidad,
          })),
        }));
        // Establece la primera asignatura como seleccionada por defecto
        if (this.result.length > 0) {
          this.selectedSubject = this.result[0].subject;
          this.selectedMedios = this.result[0].medios;
        }
      } else {
        console.error('Respuesta inválida:', response);
        this.result = [];
      }
    },
    (error) => {
      console.error('Error en la solicitud:', error);
    }
  );
}

onSubjectChange(subjectValue: string) {
  this.selectedSubject = subjectValue; // Asigna el valor seleccionado
  const selectedEntry = this.result.find(entry => entry.subject === this.selectedSubject);
  this.selectedMedios = selectedEntry ? selectedEntry.medios : [];
}
abrirModal(): void {
 
  this.cantidadReserva = 1;
  this.isModalVisible = true;
 
}
cerrarModal(event?: Event): void {
  this.isModalVisible = false;
  this.medioSeleccionado = null;

}
reservarMedio(medio: any): void {
  if (!medio || !medio.cantidadReserva) {
    alert('Debe ingresar una cantidad válida.');
    return;
  }

  if (medio.cantidadReserva < 1 || medio.cantidadReserva > medio.cantidad) {
    alert(`Cantidad inválida. Debe estar entre 1 y ${medio.cantidad}`);
    return;
  }

  // Convertimos el array a un objeto con claves dinámicas
  const reserveMeansObject = {
    [medio.medio]: medio.cantidadReserva
  };

  const requestPayload = {
    userName: this.userName,
    subjectName: this.selectedSubject,
    reserveMeans: reserveMeansObject, // Usamos el objeto en lugar del array
    reserve: true
  };
  this.solicitud.reserveClassRoomAndMeans(requestPayload).subscribe(
    response => {
      this.CargarInfo();
      medio.cantidadReserva = null;
    },
    error => {
      console.error('Error al enviar la reserva:', error);
      alert('Error al realizar la reserva');
    }
  );
}
}
