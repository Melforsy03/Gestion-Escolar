import { Component } from "@angular/core";
import { AuthGuard } from "./components/Autentificacion/auth.service";
@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent {
  title = "Gestion-Escolar";
  constructor(public authService: AuthGuard) {}

  logout() {
    this.authService.logout();
  }
}
