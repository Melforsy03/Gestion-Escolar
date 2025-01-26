import { Routes } from "@angular/router";

import { ProfesorComponent } from "../../pages/profesor-info/profesor-info.component";
import { NotasComponent } from "../../pages/notas/info-estudiantes.component";
import { TablesComponent } from "../../pages/tables/estudiante.component";
import { SolicitudComponent } from "src/app/pages/solicitud/solicitud.component";
import { InventarioComponent } from "src/app/pages/inventario/inventario.component";
import { PeticionesComponent } from "src/app/pages/peticiones/peticiones.component";
import { InfoSecretariaComponent} from "src/app/pages/info-secretaria/info-secretaria.component";
import { InfoAdministradorComponent } from "src/app/pages/info-administrador/info-administrador.component";
export const AdminLayoutRoutes: Routes = [

  { path: "profesor", component: ProfesorComponent },
  { path: "info-estudiantes", component: NotasComponent },
  { path: "estudiantes", component: TablesComponent },
  { path : "solicitar", component : SolicitudComponent},
  { path : "inventario" , component :InventarioComponent},
  { path : "peticiones" , component : PeticionesComponent },
  { path : "info-secretaria" , component :InfoSecretariaComponent},
  {path : "info-administrador" , component :InfoAdministradorComponent}
];
