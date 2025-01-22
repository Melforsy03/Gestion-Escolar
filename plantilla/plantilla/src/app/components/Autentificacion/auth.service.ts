import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { tap } from 'rxjs/operators';

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

  login(credentials: { email: string; password: string }) {
    return this.http.post('http://localhost:5164/identity/login', credentials).pipe(
      map(response => response as { token: string; role: string }),
      tap((response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', response.role);
        this.redirectUser(response.role);
      })
    );
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

  registerUser(userData: any) {
    return this.http.post('http://localhost:5164/indenity/login', userData);
  }
}
