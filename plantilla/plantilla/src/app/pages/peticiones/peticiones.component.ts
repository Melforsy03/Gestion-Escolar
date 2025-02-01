import { Component, OnInit } from '@angular/core';
import { ProfessorService } from 'src/app/service/peticiones .service';

@Component({
  selector: 'app-peticiones',
  templateUrl: './peticiones.component.html',
  styleUrls: ['./peticiones.component.css']
})
export class PeticionesComponent implements OnInit {
  specs: string[] = [];
  selectedSpec: string = '';
  professors: any[] = [];
  selectedFunctionality = 'especializacionProfesores';
  constructor(private professorService: ProfessorService) {}
  functionalities = [
    { key: 'especializacionProfesores', label: 'Gestión de Profesores por Especialización' },
    { key: 'otraFuncionalidad', label: 'Otra Funcionalidad' } // Agrega más funcionalidades aquí
  ];
  ngOnInit(): void {
    this.loadSpecs();
  }

  loadSpecs(): void {
    this.professorService.getSpecs().subscribe((response) => {
      this.specs = response.spec;
    });
  }

  onSpecChange(): void {
    if (this.selectedSpec) {
      this.professorService.getProfessorsBySpec(this.selectedSpec).subscribe((response) => {
        this.professors = response;
      });
    }
  }

  onFunctionalityChange(): void {
    console.log('Funcionalidad seleccionada:', this.selectedFunctionality);
    // Aquí puedes manejar la lógica cuando se cambia de funcionalidad
  }
  
  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
}
