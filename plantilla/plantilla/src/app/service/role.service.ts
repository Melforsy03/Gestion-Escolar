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
        { label: 'Gestionar Calificaciones', path: '/calificacion-estudiantes', icon: 'icon-paper' },
        { label: 'Info Estudiantes', path: '/estudiantes', icon: 'icon-single-copy-04' },
        { label: 'Info Profesores', path: '/profesor', icon: 'icon-notes' },
      ],
    },
    {
        role: 'administrador',
        menuItems: [
          { label: 'medios-auxiliares', path: '/medio-auxiliar', icon: 'inventory' },
          { label: 'medios-tecnologicos', path: '/medio-tecnologico', icon: 'icon-app' },
          { label :"Mantenimiento" , path : '/registrar-mantenimiento' , icon :'icon-components'}
        ],
      },
      {
        role: 'SuperAdmin',
        menuItems: [
          { label: 'Solicitud', path: '/solicitar', icon: 'icon-components' },
          { label: 'Gestionar Calificaciones', path: '/calificacion-estudiantes', icon: 'icon-paper' },
          { label :"Mantenimiento" , path : '/registrar-mantenimiento' , icon :'icon-components'},
          { label: 'Medios-auxiliares', path: '/medio-auxiliar', icon: 'icon-app' },
          { label: 'Medios Tecnologicos', path: '/medio-tecnologico', icon: 'icon-app' },
          { label: 'Evaluacion Profesores', path: '/calificaciones-profesores', icon: 'icon-notes' },
          { label: 'General Estudiantes', path: '/estudiantes-profesor', icon: 'icon-paper' },
          { label: 'Info Estudiantes', path: '/estudiantes', icon: 'icon-single-copy-04' },
          { label: 'Info Profesores', path: '/profesor', icon: 'icon-notes' },
          { label: 'Info Secretaria', path: '/info-secretaria', icon: 'icon-badge' },
          { label: 'Info Administrador', path: '/info-administrador', icon: 'icon-badge' },

          { label: 'Mantenimiento', path: '/mantenimiento', icon: 'icon-badge' },
          { label: 'Info Administrador', path: '/info-administrador', icon: 'icon-badge' },
        ],
      },
      {
        role: 'Student',
        menuItems: [
          { label: 'Evaluacion-Profesor', path: '/evaluaciones', icon: 'bx bx-chalkboard' },
        ],
      },
      
  ];

  getMenuItemsForRole(role: string): MenuItem[] {

    const permissions = this.rolePermissions.find((perm) => perm.role === role);
    return permissions ? permissions.menuItems : [];
  }

}
