import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AdminSalidasComponent} from "../admin-salidas/admin-salidas.component";

@Component({
  selector: 'app-inicio-admin',
  standalone: true,
  imports: [CommonModule, AdminSalidasComponent],
  templateUrl: './inicio-admin.component.html',
  styleUrl: './inicio-admin.component.css'
})
export class InicioAdminComponent {

}
