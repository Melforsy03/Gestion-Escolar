import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TechnologicalMeansService, TechnologicalMeans } from 'src/app/service/mediosTecnologico.service';

@Component({
  selector: 'app-medio-tecnologico',
  templateUrl: './medios-tecnologicos.component.html',
  styleUrls: ['./medio-tecnologicos.css'],
})
export class MediosTecnologicosComponent implements OnInit {
  medios: TechnologicalMeans[] = [];
  medioForm: FormGroup;
  isModalVisible = false; // Controla la visibilidad del modal

  constructor(private fb: FormBuilder, private medioService: TechnologicalMeansService) {}

  ngOnInit(): void {
    this.medioForm = this.fb.group({
      nameMean: ['', Validators.required],
      state: ['', Validators.required],
    });

    this.loadMedios();
  }

  loadMedios(): void {
    this.medioService.listTechnologicalMeans().subscribe(
      (response) => {
        this.medios = response;
      },
      (error) => {
        console.error('Error al cargar los medios:', error);
      }
    );
  }

  onSubmit(): void {
    if (this.medioForm.valid) {
      const nuevoMedio: TechnologicalMeans = {
        idMean: undefined, // La API generará el ID automáticamente
        nameMean: this.medioForm.get('nameMean')?.value,
        state: this.medioForm.get('state')?.value,
      };
      console.log('Datos del formulario (raw values):', this.medioForm.value);
      console.log('Nuevo medio (mapeado):', nuevoMedio);

      this.medioService.createTechnologicalMeans(nuevoMedio).subscribe(
        (response) => {
          console.log('Medio creado con éxito:', response);
          this.medios.push(response);
          this.toggleModal(); // Cierra el modal tras guardar
        },
        (error) => {
          console.error('Error al crear el medio:', error);
        }
      );
    } else {
      console.warn('El formulario no es válido');
    }
  }

  deleteMedio(medio: TechnologicalMeans): void {
    if (medio.idMean) {
      if (confirm(`¿Estás seguro de que deseas eliminar el medio "${medio.nameMean}"?`)) {
        this.medioService.deleteTechnologicalMeans(medio.idMean).subscribe(
          () => {
            console.log(`Medio eliminado con éxito: ${medio.nameMean}`);
            this.medios = this.medios.filter((m) => m.idMean !== medio.idMean);
          },
          (error) => {
            console.error('Error al eliminar el medio:', error);
            alert('No se pudo eliminar el medio. Por favor, inténtalo de nuevo.');
          }
        );
      }
    } else {
      console.warn('El medio no tiene un ID válido y no se puede eliminar:', medio);
    }
  }

  toggleModal(): void {
    this.isModalVisible = !this.isModalVisible;
    if (!this.isModalVisible) {
      this.medioForm.reset(); // Resetea el formulario al cerrar el modal
    }
  }

  closeModal(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (target.classList.contains('modal')) {
      this.toggleModal();
    }
  }
}
