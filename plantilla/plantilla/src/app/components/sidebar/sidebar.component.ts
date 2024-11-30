import { Component, OnInit } from "@angular/core";
import { RoleService } from "src/app/service/role.service";
import { MenuItem, } from "src/app/models/role-permissions.model";
import { AuthGuard } from "../Autentificacion/auth.service";
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  {
    path: "/dashboard",
    title: "Dashboard",
    icon: "icon-chart-pie-36",
    class: ""
  },
  {
    path: "/profesor",
    title: "Profesor-Informacion",
    icon: "icon-atom",
    class: ""
  },
  {
    path: "/notas",
    title: "Notas-Asignatura",
    icon: "icon-bell-55",
    class: ""
  },
  {
    path: "/tables",
    title: "Informacion de estudiantes",
    icon: "icon-puzzle-10",
    class: ""
  },
];

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.css"]
})
export class SidebarComponent implements OnInit {
  menuItems: MenuItem[] = [];

  constructor(private roleService: RoleService , private authGuard :AuthGuard) {}

  ngOnInit():void {
    const userRole = this.authGuard.getUserRole(); // Implementa este método en AuthService
    // Aquí debes obtener el rol dinámicamente, esto es solo un ejemplo
    this.menuItems = this.roleService.getMenuItemsForRole(userRole);
  }

  isMobileMenu() {
    if (window.innerWidth > 991) {
      return false;
    }
    return true;
  }
}
