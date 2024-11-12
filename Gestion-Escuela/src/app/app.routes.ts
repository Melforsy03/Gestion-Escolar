// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './autentificacion/autentificacion.component';
import { AdministradorComponent } from './administrador/administrador.component';
import { ProfesorComponent } from './profesor/profesor.component';
import { SecretariaComponent } from './secretaria/secretaria.component';
import { DecanoComponent } from './decano/decano.component';
import { AuthGuard } from './autentificacion/auth.service';
export const routes: Routes = [
  {
    path: 'profesor',
    component: ProfesorComponent,
    canActivate: [AuthGuard],
    data: { role: 'profesor' },
  },
  {
    path: 'administrador',
    component: AdministradorComponent,
    canActivate: [AuthGuard],
    data: { role: 'administrador' },
  },
  {
    path: 'secretaria',
    component: SecretariaComponent,
    canActivate: [AuthGuard],
    data: { role: 'secretaria' },
  },
  {
    path: 'decano',
    component: DecanoComponent,
    canActivate: [AuthGuard],
    data: { role: 'decano' },
  },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'login', component: AuthComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes) ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
