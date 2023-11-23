import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-admin-ventas',
  standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-ventas.component.html',
  styleUrl: './admin-ventas.component.css'
})
export class AdminVentasComponent {

}
