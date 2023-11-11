
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {InicioComponent} from "./inicio/inicio.component";
import {HttpClient, HttpClientModule, HttpHeaders} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, InicioComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Front-End';
}
