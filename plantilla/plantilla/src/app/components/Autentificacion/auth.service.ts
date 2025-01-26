import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  private isLoggedIn = false;
  private userRole: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const userRole = localStorage.getItem('role');
    const expectedRole = route.data['role'];
    const token  = localStorage.getItem('token');
    if (userRole === expectedRole) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }

  
  login(credentials: { userName: string; password: string }) {
    console.log("Intentando iniciar sesión...");
    return this.http.post('http://localhost:5164/identity/login', credentials).pipe(
      tap(response => {
        console.log("Respuesta completa del servidor:");
        console.dir(response, { depth: null });
      }),
      map(response => {
        console.log("Respuesta mapeada:");
        console.dir(response, { depth: null });
        return response as { verificstion: boolean, role: string , token: string };
      }),
      tap((response) => {
        console.log("Respuesta en tap:");
        console.dir(response, { depth: null });
        
        const role = response.role;
        const token = response.token;
        localStorage.setItem('role', role);
        localStorage.setItem('token',token);
        this.isLoggedIn = true;
        console.log("Rol obtenido:", role);
        
        // Aquí llamamos a redirectUser con el rol
        this.redirectUser(role);
      }),
     )
    ;
  }
 
  logout() {
    this.isLoggedIn = false;
    this.userRole = null;
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }
  getToken () : string | null{
    return localStorage.getItem('token');
  }
  getUserRole(): string | null {
    console.log("llego")
    return localStorage.getItem('role');
  }

  redirectUser(role: string) {
    switch (role) {
      case 'professor':
        this.router.navigate(['/profesor']);
        break;
      case 'administrador':
        this.router.navigate(['/administrador']);
        break;
      case 'Secretary':
        this.router.navigate(['/secretaria']);
        break;
      case 'SuperAdmin':
        this.router.navigate(['/SuperAdmin']);
        break;
      default:
        this.router.navigate(['/login']);

        break;
    }
  }
}