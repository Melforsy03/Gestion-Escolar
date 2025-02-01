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

  maintenances: any = {};  // Para almacenar los mantenimientos
  totalMaintenances: number = 0;  // Para almacenar la cantidad total de mantenimientos

  constructor(private professorService: ProfessorService) {}

  functionalities = [
    { key: 'especializacionProfesores', label: 'Gestión de Profesores por Especialización' },
    { key: 'mantenimientosRealizados', label: 'Mantenimientos Realizados' } // Nueva funcionalidad
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
    if (this.selectedFunctionality === 'mantenimientosRealizados') {
      this.loadMaintenances();
    }
  }

  loadMaintenances(): void {
    this.professorService.getMaintenances().subscribe((response) => {
      this.maintenances = response.classRoomsAndMeans;
      this.totalMaintenances = response.ammountOfMaintenance2yo;
    });
  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
}
