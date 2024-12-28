import { Routes } from "@angular/router";

import { DashboardComponent } from "../../pages/dashboard/dashboard.component";
import { ProfesorComponent } from "../../pages/profesor-info/profesor-info.component";
import { NotasComponent } from "../../pages/notas/info-estudiantes.component";
import { TablesComponent } from "../../pages/tables/estudiante.component";
import { SolicitudComponent } from "src/app/pages/solicitud/solicitud.component";
import { InventarioComponent } from "src/app/pages/inventario/inventario.component";
export const AdminLayoutRoutes: Routes = [
  { path: "dashboard", component: DashboardComponent },
  { path: "profesor", component: ProfesorComponent },
  { path: "info-estudiantes", component: NotasComponent },
  { path: "estudiantes", component: TablesComponent },
  { path : "solicitar", component : SolicitudComponent},
  { path : "inventario" , component :InventarioComponent}
];
