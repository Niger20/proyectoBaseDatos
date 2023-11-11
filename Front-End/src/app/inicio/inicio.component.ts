import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpClientModule} from "@angular/common/http";

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {
  arregloCuartos: Cuartos[] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void{
    this.obtenerDatosCuarto();
  }

  obtenerDatosCuarto() {
    const url = "https://localhost:44308/api/CuartosFrios"

    this.http.get<CuartosResponse>(url).subscribe(
      (response) => {
        if (Array.isArray(response.data)){
          this.arregloCuartos = response.data
          console.log(this.arregloCuartos)
        }
      },
      (error) => {
        console.error("Error al obtener datos: ", error)
      }
    )
  }
}

export class Cuartos {
  idCuarto: number = 0;
  capacidadMaxima: number = 0;
  cantidadActual: number = 0;
  capacidadDisponible: number = 0;
}

export class CuartosResponse{
  data: string = ""
}
