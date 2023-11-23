import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-admin-entradas',
  standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-entradas.component.html',
  styleUrl: './admin-entradas.component.css'
})
export class AdminEntradasComponent {

}
