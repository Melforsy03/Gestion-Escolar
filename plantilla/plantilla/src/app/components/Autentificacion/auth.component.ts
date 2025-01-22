// auth.component.ts
import { Component } from '@angular/core';


@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent {
  // Título de la aplicación
  title = 'Escuela';

  // Estilos para controlar la visibilidad de los formularios
  loginStyles = { left: '4px', opacity: '1' };
  registerStyles = { right: '-520px', opacity: '0' };

  // Estado del menú responsivo
  isMenuResponsive = false;

  // Función para mostrar el formulario de inicio de sesión
  showLogin() {
    this.loginStyles = { left: '4px', opacity: '1' };
    this.registerStyles = { right: '-520px', opacity: '0' };
  }

  // Función para alternar el menú
  toggleMenu() {
    this.isMenuResponsive = !this.isMenuResponsive;
  }
}
