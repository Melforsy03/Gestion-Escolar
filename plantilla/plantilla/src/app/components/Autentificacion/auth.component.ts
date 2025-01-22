// auth.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthGuard } from './auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})

export class AuthComponent {
  loginForm: FormGroup;
  userData = { email: '', password: '' };
  rememberMe = false;
  constructor(private fb: FormBuilder, private authGuard: AuthGuard) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

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
  onSubmit() {
    if (this.loginForm.valid) {
      this.userData.email = this.loginForm.get('email')?.value;
      this.userData.password = this.loginForm.get('password')?.value;
      
       this.authGuard.registerUser(this.userData).subscribe(
        response => console.log(response),
        error => console.error(error)
      );
    }
 }
}
