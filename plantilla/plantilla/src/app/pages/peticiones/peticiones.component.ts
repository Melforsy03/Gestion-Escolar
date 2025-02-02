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

  maintenances: any = {};
  totalMaintenances: number = 0;
  goodProfesor: any[] = [];
  badStudents: any[] = [];
  punishedProfessors: any[] = [];
  maintenanceCosts: any[] = [];

  constructor(private professorService: ProfessorService) {}

  functionalities = [
    { key: 'especializacionProfesores', label: 'Gestión de Profesores por Especialización' },
    { key: 'mantenimientosRealizados', label: 'Mantenimientos Realizados' },
    { key: 'ProfesorBuenaNota', label: 'Profesores con Buena Nota' },
    { key: 'BadStudents', label: 'Estudiantes bajo rendimiento' },
    { key: 'BadProfessor', label: 'Profesor con mala Nota' },
    { key: 'promedioMantenimiento', label: 'Promedio Mantenimiento' } // Nueva funcionalidad
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
    } else if (this.selectedFunctionality === 'ProfesorBuenaNota') {
      this.loadGoodProfesor();
    } else if (this.selectedFunctionality === 'BadProfessor') {
      this.loadBadProfesor();
    } else if (this.selectedFunctionality === 'BadStudents') {
      this.loadBadStudents();
    } else if (this.selectedFunctionality === 'promedioMantenimiento') {
      this.loadMaintenanceCosts(); // Llamada a la nueva funcionalidad
    }
  }

  loadMaintenances(): void {
    this.professorService.getMaintenances().subscribe((response) => {
      this.maintenances = response.classRoomsAndMeans;
      this.totalMaintenances = response.ammountOfMaintenance2yo;
    });
  }
loadGoodProfesor () :void
{
  this.professorService.getGoodProfessors().subscribe(response => {
    this.goodProfesor = Object.entries(response.professorsAndSubjects).map(
      ([name, subjects]) => ({ name, subjects })
    );
  });
  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
  loadBadStudents() :void {
    this.professorService.getBadStudents().subscribe(response => {
      this.badStudents = response
    });
}
loadBadProfesor() : void
{
  this.professorService.getBadProfesor().subscribe(response => {
    this.punishedProfessors = response.map(professor => ({
      nameProf: professor.nameProf,
      punishmentDate: professor.punishmentDate,
      UseAuxMean: professor.UseAuxMean,
      evals: professor.evals.length > 0 ? professor.evals : [0, 0, 0]
    }));
  });

}
loadMaintenanceCosts(): void {
  this.professorService.getMaintenanceCosts().subscribe(response => {
    console.log("Datos de mantenimiento recibidos:", response); // Log para depuración
    // Asignamos la propiedad classRoomAverageCost al array de mantenimiento
    this.maintenanceCosts = Object.entries(response.classRoomAverageCost).map(([idClassR, amount]) => ({
      idClassR,      // ID del aula
      amount         // Costo asociado a ese aula
    }));
  });
}


}

