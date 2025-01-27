import { Injectable } from '@angular/core';
import { RolePermissions, MenuItem } from '../models/role-permissions.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
   private rolePermissions: RolePermissions[] = [
    {
      role: 'professor',
      menuItems: [
        { label: 'Solicitud', path: '/solicitar', icon: 'bx bx-chalkboard' },
        { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-edit' },
        { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'bx bx-edit' },
      ],
    },
    {
      role: 'Secretary',
      menuItems: [
        { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-user' },
        { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'bx bx-edit' },
        { label: 'Informacion-profesores', path: '/profesor', icon: 'bx bx-edit' },
      ],
    },
    {
        role: 'administrador',
        menuItems: [
          { label: 'medios-auxiliares', path: '/medio-auxiliar', icon: 'inventory' },
        ],
      },
      {
        role: 'SuperAdmin',
        menuItems: [
          { label: 'Solicitud', path: '/solicitar', icon: 'bx bx-chalkboard' },
          { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'bx bx-user' },
          { label: 'medios-auxiliares', path: '/medio-auxiliar', icon: 'inventory' },
          { label: 'medios-tecnologicos', path: '/medio-tecnologico', icon: 'inventory' },
          { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'person' },
          { label: 'Información Profesores', path: '/profesor', icon: 'person' },
          { label: 'info-secretaria', path: '/info-secretaria', icon: 'bx bx-user' },
          { label: 'info-administrador', path: '/info-administrador', icon: 'bx bx-user' },
          { label: 'Peticiones', path: '/peticiones', icon: 'bx bx-user' },

        ],
      },
  ];

  getMenuItemsForRole(role: string): MenuItem[] {
    
    const permissions = this.rolePermissions.find((perm) => perm.role === role);
    return permissions ? permissions.menuItems : [];
  }
  
}
