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

    if (userRole === expectedRole) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }

  private decryptToken(token: string): string {
    const decrypted = CryptoJS.AES.decrypt(token, 'tuClaveSecreta').toString(CryptoJS.enc.Utf8);
    return decrypted.toString();
  }
  login(credentials: { email: string; password: string }) {
    return this.http.post('http://localhost:5164/identity/login', credentials).pipe(
      map(response => response as { valido: boolean, token: string }),
      tap((response) => {
        const decryptedToken = this.decryptToken(response.token);
        localStorage.setItem('valido', String(response.valido));
        localStorage.setItem('token', decryptedToken);
        const role = this.getRoleFromToken(response);
        localStorage.setItem('role', role);
        this.redirectUser(role);
      })
    );
  }
  private getRoleFromToken(response: { token: string }): string {
    // Extraer el token encriptado del objeto de respuesta
    const encryptedToken = response.token;
  
    // Desencriptar el token
    const decryptedToken = this.decryptToken(encryptedToken);
  
    // Extraer el rol del token desencriptado
    const roleMatch = decryptedToken.match(/role:(\w+)/);
    if (roleMatch && roleMatch[1]) {
      return roleMatch[1];
    }
    // Si no se encuentra el rol, retorna uno por defecto
    return 'profesor';
  }
  logout() {
    this.isLoggedIn = false;
    this.userRole = null;
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }

  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

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
