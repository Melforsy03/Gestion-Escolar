export interface RolePermissions {
    role: string;
    menuItems: MenuItem[];
  }
  
  export interface MenuItem {
    label: string;
    route: string;
    icon?: string; // Este campo es opcional
  }
