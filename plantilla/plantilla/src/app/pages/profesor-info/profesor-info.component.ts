import { Component, OnInit } from '@angular/core';
import { ProfesorService } from 'src/app/service/profesor-info.service';

@Component({
  selector: 'app-profesor-info',
  templateUrl: './profesor-info.component.html',
  styleUrls: ['./profesor-info.component.css']
})
export class ProfesorComponent implements OnInit {
  profesores: any[] = [];
  globalSearchQuery: string = '';
  showModal: boolean = false; // Controla la visibilidad del modal
  newProfesor: any = { nameProf: '', contract: '', salary: 0, laboralExperience: 0 }; // Datos del nuevo profesor

  constructor(private profesorService: ProfesorService) {}

  ngOnInit(): void {
    this.loadProfesores();
  }

  loadProfesores(): void {
    this.profesorService.listProfesores().subscribe(
      (data) => {
        this.profesores = data.map((profesor: any) => ({
          ...profesor,
          isEditing: false // Agregar propiedad para controlar el estado de edición
        }));
      },
      (error) => {
        console.error('Error al listar profesores:', error);
      }
    );
  }

  openModal(): void {
    console.log('Modal abierto'); // Depuración
    this.showModal = true;
    console.log(this.showModal)
    this.newProfesor = { nameProf: '', contract: '', salary: 0, laboralExperience: 0 }; // Limpiar el formulario
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveProfesor(): void {
    this.profesorService.createProfesor(this.newProfesor).subscribe(
      (response) => {
        console.log('Profesor agregado:', response);
        this.loadProfesores();
        this.closeModal();
      },
      (error) => {
        console.error('Error al agregar profesor:', error);
      }
    );
  }

  toggleEdit(profesor: any): void {
    profesor.isEditing = !profesor.isEditing; // Cambiar el estado de edición
  }

  saveChanges(profesor: any): void {
    if (!profesor.id) {
      // Crear nuevo profesor
      this.profesorService.createProfesor(profesor.professor).subscribe(
        (response) => {
          console.log('Profesor agregado:', response);
          this.loadProfesores();
        },
        (error) => {
          console.error('Error al agregar profesor:', error);
        }
      );
    } else {
      // Actualizar profesor existente
      const updatedProfesor = {
        id: profesor.id,
        professor: {
          nameProf: profesor.professor.nameProf,
          contract: profesor.professor.contract,
          salary: profesor.professor.salary,
          laboralExperience: profesor.professor.laboralExperience
        }
      };

      this.profesorService.updateProfesor(updatedProfesor).subscribe(
        (response) => {
          console.log('Profesor actualizado:', response);
          profesor.isEditing = false; // Salir del modo de edición
        },
        (error) => {
          console.error('Error al actualizar profesor:', error);
        }
      );
    }
  }

  deleteProfesor(id: number): void {
    this.profesorService.deleteProfesor(id).subscribe(
      (response) => {
        console.log('Profesor eliminado:', response);
        this.loadProfesores();
      },
      (error) => {
        console.error('Error al eliminar profesor:', error);
      }
    );
  }

  updateDisplayProfesor(): void {
    // Filtrar la lista de profesores según la consulta de búsqueda global.
    this.profesores = this.profesores.filter(profesor =>
      profesor.professor.nameProf.toLowerCase().includes(this.globalSearchQuery.toLowerCase())
    );
  }
}
