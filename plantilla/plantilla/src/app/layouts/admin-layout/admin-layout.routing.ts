
import { Routes } from "@angular/router";
import { ProfesorComponent } from "../../pages/profesor-info/profesor-info.component";
import { InfoEstudiantesComponent } from "../../pages/notas/info-estudiantes.component";
import { TablesComponent } from "../../pages/tables/estudiante.component";
import { SolicitudComponent } from "src/app/pages/solicitud/solicitud.component";
import {MedioAuxiliarComponent } from "src/app/pages/medios-auxiliares/mediosAuxiliares.component";
import { PeticionesComponent } from "src/app/pages/peticiones/peticiones.component";
import { InfoSecretariaComponent} from "src/app/pages/info-secretaria/info-secretaria.component";
import { InfoAdministradorComponent } from "src/app/pages/info-administrador/info-administrador.component";
import { MediosTecnologicosComponent } from "src/app/pages/medios-tecnologicos/medios-tecnologicos.component";
import { CalificacionesComponent } from "src/app/pages/calificaciones-profesores/calificacionesProfesores.component";
import { EstudiantesProfesor } from "src/app/pages/estudiantes-profesor/estudiantes-pofesor.component";
import { EvaluacionOtorgadaComponent } from "src/app/pages/evaluacion-otorgada/evaluacion-otorgada.component";
import { RegistarMantenimientoComponent } from "src/app/pages/registar-mantenimiento/registar-mantenimiento.component";
export const AdminLayoutRoutes: Routes = [

  { path: "profesor", component: ProfesorComponent },
  { path: "calificacion-estudiantes", component: InfoEstudiantesComponent },
  { path: "estudiantes", component: TablesComponent },
  { path : "solicitar", component : SolicitudComponent},
  { path : "medio-auxiliar" , component :MedioAuxiliarComponent},
  { path : "peticiones" , component : PeticionesComponent },
  { path : "info-secretaria" , component :InfoSecretariaComponent},
  { path : "info-administrador" , component :InfoAdministradorComponent},
  { path : "medio-tecnologico" , component :MediosTecnologicosComponent},
  { path : "calificaciones-profesores" , component :CalificacionesComponent},
  { path : "estudiantes-profesor" , component :EstudiantesProfesor},
  { path : "evaluacion-otorgada" , component :EvaluacionOtorgadaComponent},
  { path :  "registrar-mantenimiento" , component : RegistarMantenimientoComponent}
];
