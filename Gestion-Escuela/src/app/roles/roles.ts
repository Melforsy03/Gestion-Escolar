import { Component, OnInit } from '@angular/core';
import { AuthGuard } from '../autentificacion/auth.service';

@Component({
  selector: 'app-some-component',
  templateUrl: './some-component.component.html',
})
export class SomeComponent implements OnInit {
  userRole: string | null = '';

  constructor(private authService: AuthGuard) {}

  ngOnInit() {
    this.userRole = this.authService.getUserRole();
    // Puedes usar `this.userRole` para mostrar contenido específico
  }
}
