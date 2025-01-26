 // Ajusta la ruta según la ubicación de tu interceptor

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from "./app.component";
import { AdminLayoutComponent } from "./layouts/admin-layout/admin-layout.component";
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AppRoutingModule } from "./app-routing.module";
import { ComponentsModule } from "./components/components.module";
import { StudentService } from "./pages/tables/estudiante.service";
import { AuthComponent } from "./components/Autentificacion/auth.component";
import { CommonModule } from '@angular/common';
import { SolicitudComponent } from './pages/solicitud/solicitud.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InventarioComponent } from './pages/inventario/inventario.component';
import { PeticionesComponent } from './pages/peticiones/peticiones.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    CommonModule,
    ReactiveFormsModule,


  ],
  declarations: [AppComponent, AdminLayoutComponent, AuthLayoutComponent,AuthComponent, SolicitudComponent, InventarioComponent, PeticionesComponent],
//ModificadoTOKEN
  providers: [
    StudentService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true, // Permite que se puedan usar múltiples interceptores si fuera necesario
    }
],
  bootstrap: [AppComponent]
})
export class AppModule {}
