import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Obtener el token del localStorage
    const token = localStorage.getItem('token');

    // Si el token existe, añadirlo al encabezado de la solicitud
    if (token) {
      const clonedRequest = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}` // Agregar el token en el encabezado
        }
      });

      return next.handle(clonedRequest);
    }

    // Si no hay token, continuar con la solicitud sin modificarla
    return next.handle(request);
  }
}
