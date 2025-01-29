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
  result: any = null; // Para almacenar los resultados del endpoint
  userName: string = '';
  selectedSubject: string = ''; // Asignatura seleccionada
  selectedMedios: any[] = []; // Medios para la asignatura seleccionada
  constructor(private fb: FormBuilder, private http: HttpClient , private solicitud :SolicitudService) {
    this.solicitudForm = this.fb.group({
      aula: [''],
      medios: this.fb.array([]),
    });
  }

  get medios() {
    return this.solicitudForm.get('medios') as FormArray;
  }

  addMedio() {
    const medioForm = this.fb.group({
      nombre: [''],
      cantidad: ['']
    });
    this.medios.push(medioForm);
  }

  removeMedio(index: number) {
    this.medios.removeAt(index);
  }
ngOnInit(): void {
    this.CargarInfo();
}
reservarMedio(medio: any) {
  console.log('Reservando medio:', medio);
  alert(`Has reservado ${medio.medio} (${medio.cantidad} disponibles).`);

  // Aquí puedes llamar al servicio para realizar la reserva en el backend
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


} 