import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-solicitud',
  templateUrl: './solicitud.component.html',
  styleUrls: ['./solicitud.component.css'],
 
})
export class SolicitudComponent implements OnInit {
  solicitudForm: FormGroup;
  result: any = null; // Para almacenar los resultados del endpoint

  constructor(private fb: FormBuilder, private http: HttpClient) {
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
    
}
  onSubmit() {
    const userName = 'ProfesorX'; // Reemplazar con el nombre del profesor dinámico si es necesario
    const payload = { userName };

    this.http.post('http://localhost:3000/checkAviableClassRoomsAndMeans', payload).subscribe(
      (response) => {
        this.result = response; // Guardar la respuesta para visualizarla
      },
      (error) => {
        console.error('Error en la solicitud:', error);
      }
    );
  }
}