
// app.component.ts
import { Component } from '@angular/core';
import { AuthComponent } from './autentificacion/autentificacion.component';
import { AuthGuard } from './autentificacion/auth.service';
import { CommonModule } from '@angular/common'; 
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [AuthComponent,CommonModule,RouterModule]
})
export class AppComponent {
  title ='Escuela';
  constructor(public authService: AuthGuard) {}

  logout() {
    this.authService.logout();
  }
}