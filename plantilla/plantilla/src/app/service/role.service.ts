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
        { label: 'Solicitud', route: '/solicitar', icon: 'bx bx-chalkboard' },
        { label: 'Gestionar Calificaciones', route: '/notas', icon: 'bx bx-edit' },
        { label: 'Informacion-Estudiantes', route: '/tables', icon: 'bx bx-edit' },
      ],
    },
    {
      role: 'secretaria',
      menuItems: [
        { label: 'Gestionar Calificaciones', route: '/notas', icon: 'bx bx-user' },
        { label: 'Informacion-Estudiantes', route: '/tables', icon: 'bx bx-edit' },
        { label: 'Informacion-profesores', route: '/profesor', icon: 'bx bx-edit' },
      ],
    },
    {
        role: 'administrador',
        menuItems: [
          { label: 'Inventario', route: '/inventario', icon: 'inventory' },
        ],
      },
      {
        role: 'decano',
        menuItems: [
          { label: 'Solicitud', route: '/solicitar', icon: 'bx bx-chalkboard' },
          { label: 'Inventario', route: '/inventario', icon: 'inventory' },
          { label: 'Información Profesores', route: '/profesor', icon: 'person' },
          { label: 'Informacion-Estudiantes', route: '/tables', icon: 'person' },
          { label: 'Gestionar Calificaciones', route: '/notas', icon: 'bx bx-user' },
        ],
      },
  ];

  getMenuItemsForRole(role: string): MenuItem[] {
    const permissions = this.rolePermissions.find((perm) => perm.role === role);
    return permissions ? permissions.menuItems : [];
  }
}
