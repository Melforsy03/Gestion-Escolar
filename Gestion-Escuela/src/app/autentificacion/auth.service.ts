import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router  } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  private isLoggedIn = false; // Controla si el usuario está logueado
  private userRole: string | null = null; // Almacena el rol del usuario

  constructor(private http: HttpClient, private router: Router) {}

  // Método que Angular llama al intentar acceder a una ruta protegida
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const userRole = localStorage.getItem('role'); // Obtener rol del usuario desde el almacenamiento local
    const expectedRole = route.data['role']; // Obtener el rol esperado para la ruta

    if (userRole === expectedRole) { // Si el rol del usuario coincide, permite el acceso
      return true;
    } else { // Si no coincide, redirige a login
      this.router.navigate(['/login']);
      return false;
    }
  }

  // Método para iniciar sesión y guardar el rol del usuario y el token en localStorage
  login(credentials: { email: string; password: string }) {
    this.http.post('/api/login', credentials).subscribe((res: any) => {
      localStorage.setItem('token', res.token); // Guardar el token
      localStorage.setItem('role', res.role); // Guardar el rol
      this.redirectUser(res.role); // Redirigir basado en el rol
    });
  }

  // Método para cerrar sesión
  logout() {
    this.isLoggedIn = false;
    this.userRole = null;
  }

  // Método que verifica si el usuario está autenticado
  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }

  // Método para obtener el rol del usuario
  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  // Método para redirigir basado en el rol del usuario
  redirectUser(role: string) {
    switch (role) {
      case 'profesor':
        this.router.navigate(['/profesor']);
        break;
      case 'administrador':
        this.router.navigate(['/administrador']);
        break;
      case 'secretaria':
        this.router.navigate(['/secretaria']);
        break;
      case 'decano':
        this.router.navigate(['/decano']);
        break;
      default:
        this.router.navigate(['/login']);
        break;
    }
  }
}
