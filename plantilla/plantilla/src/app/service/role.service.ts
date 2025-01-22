import { Injectable } from '@angular/core';
import { RolePermissions, MenuItem } from '../models/role-permissions.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
   private rolePermissions: RolePermissions[] = [
    {
      role: 'profesor',
      menuItems: [
        { label: 'Solicitud', path: '/solicitar', icon: 'bx bx-chalkboard' },
        { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-edit' },
        { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'bx bx-edit' },
      ],
    },
    {
      role: 'secretaria',
      menuItems: [
        { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-user' },
        { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'bx bx-edit' },
        { label: 'Informacion-profesores', path: '/profesor', icon: 'bx bx-edit' },
      ],
    },
    {
        role: 'administrador',
        menuItems: [
          { label: 'Inventario', path: '/inventario', icon: 'inventory' },
        ],
      },
      {
        role: 'decano',
        menuItems: [
          { label: 'Solicitud', path: '/solicitar', icon: 'bx bx-chalkboard' },
          { label: 'Inventario', path: '/inventario', icon: 'inventory' },
          { label: 'Información Profesores', path: '/profesor', icon: 'person' },
          { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'person' },
          { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-user' },
          { label: 'Peticiones', path: '/peticiones', icon: 'bx bx-user' },
        ],
      },
  ];

  getMenuItemsForRole(role: string): MenuItem[] {
    
    const permissions = this.rolePermissions.find((perm) => perm.role === 'decano');
    return permissions ? permissions.menuItems : [];
  }
  
}
