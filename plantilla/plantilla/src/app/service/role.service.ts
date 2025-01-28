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
          { label: 'Solicitud', path: '/solicitar', icon: 'icon-components' },
          { label: 'Gestionar Calificaciones', path: '/info-estudiantes', icon: 'icon-paper' },
          { label: 'medios-auxiliares', path: '/medio-auxiliar', icon: 'icon-app' },
          { label: 'medios-tecnologicos', path: '/medio-tecnologico', icon: 'icon-app' },
          { label: 'Informacion-Estudiantes', path: '/estudiantes', icon: 'icon-single-copy-04' },
          { label: 'Información Profesores', path: '/profesor', icon: 'icon-notes' },
          { label: 'info-secretaria', path: '/info-secretaria', icon: 'icon-badge' },
          { label: 'Peticiones', path: '/peticiones', icon: 'icon-bullet-list-67'},
          { label:  'calificaciones profesores', path: '/calificaciones-profesores', icon: 'icon-notes' },
          { label:  'estudiantes profesor', path: '/estudiantes-profesor', icon: 'icon-paper' },
          { label: ' info-administrador', path: '/info-administrador', icon: 'icon-badge' },
        ],
      },
  ];

  getMenuItemsForRole(role: string): MenuItem[] {

    const permissions = this.rolePermissions.find((perm) => perm.role === role);
    return permissions ? permissions.menuItems : [];
  }

}
