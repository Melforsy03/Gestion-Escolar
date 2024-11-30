import { Routes } from "@angular/router";

import { DashboardComponent } from "../../pages/dashboard/dashboard.component";
import { ProfesorComponent } from "../../pages/profesor-info/profesor-info.component";
import { NotasComponent } from "../../pages/notas/notas.component";
import { TablesComponent } from "../../pages/tables/tables.component";

export const AdminLayoutRoutes: Routes = [
  { path: "dashboard", component: DashboardComponent },
  { path: "profesor", component: ProfesorComponent },
  { path: "notas", component: NotasComponent },
  { path: "tables", component: TablesComponent },
];
