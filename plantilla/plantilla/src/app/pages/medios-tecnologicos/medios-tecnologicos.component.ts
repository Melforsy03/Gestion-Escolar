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
  isModalVisible = false; // Controla la visibilidad del modal principal
  isEditModalVisible = false; // Controla la visibilidad del modal de edición
  selectedMedio: TechnologicalMeans | null = null; // Medio seleccionado para editar

  constructor(private fb: FormBuilder, private medioService: TechnologicalMeansService) {}

  ngOnInit(): void {
    // Inicialización del formulario
    this.medioForm = this.fb.group({
      nameMean: ['', Validators.required],
      state: ['', Validators.required],
    });

    // Cargar los medios tecnológicos al iniciar el componente
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
        nameMean: this.medioForm.get('nameMean')?.value,
        state: this.medioForm.get('state')?.value,
      };

      this.medioService.createTechnologicalMeans(nuevoMedio).subscribe(
        (response) => {
          console.log('Medio creado con éxito:', response);
          this.medios.push(response); // Añadir el nuevo medio a la lista
          this.toggleModal(); // Cierra el modal
        },
        (error) => {
          console.error('Error al crear el medio:', error);
        }
      );
    } else {
      console.warn('Formulario inválido');
    }
  }

  deleteMedio(medio: TechnologicalMeans): void {
    if (medio.idMean) {
      const confirmacion = confirm(`¿Estás seguro de eliminar el medio "${medio.nameMean}"?`);
      if (confirmacion) {
        this.medioService.deleteTechnologicalMeans(medio.idMean).subscribe(
          () => {
            console.log(`Medio "${medio.nameMean}" eliminado con éxito.`);
            this.medios = this.medios.filter((m) => m.idMean !== medio.idMean);
          },
          (error) => {
            console.error('Error al eliminar el medio:', error);
            alert('No se pudo eliminar el medio. Intenta nuevamente.');
          }
        );
      }
    }
  }

  editMedio(medio: TechnologicalMeans): void {
    this.selectedMedio = { ...medio }; // Clonar el objeto seleccionado
    this.isEditModalVisible = true;
    this.medioForm.patchValue({
      nameMean: medio.nameMean,
      state: medio.state,
    });
  }

  updateMedio(): void {
    if (this.selectedMedio && this.medioForm.valid) {
      this.selectedMedio.nameMean = this.medioForm.get('nameMean')?.value;
      this.selectedMedio.state = this.medioForm.get('state')?.value;

      this.medioService.updateTechnologicalMeans(this.selectedMedio).subscribe(
        (updatedMedio) => {
          console.log('Medio actualizado:', updatedMedio);
          const index = this.medios.findIndex((m) => m.idMean === updatedMedio.idMean);
          if (index !== -1) {
            this.medios[index] = updatedMedio; // Actualiza la lista con el medio editado
          }
          this.closeEditModal();
        },
        (error) => {
          console.error('Error al actualizar el medio:', error);
        }
      );
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

  toggleEditModal(): void {
    this.isEditModalVisible = !this.isEditModalVisible;
  }

  closeEditModal(event?: MouseEvent): void {
    if (event) {
      const target = event.target as HTMLElement;
      if (!target.classList.contains('modal')) {
        return;
      }
    }
    this.isEditModalVisible = false;
    this.selectedMedio = null; // Limpia el medio seleccionado
    this.medioForm.reset(); // Resetea el formulario
  }
}
