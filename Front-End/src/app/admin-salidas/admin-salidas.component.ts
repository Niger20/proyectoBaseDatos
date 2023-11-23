import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule, NgForm} from "@angular/forms";


@Component({
  selector: 'app-admin-salidas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-salidas.component.html',
  styleUrl: './admin-salidas.component.css'
})
export class AdminSalidasComponent {

  arregloDatos: Salidas [] = [];

  modeloSalida : Salidas = new Salidas()

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  formatearFecha(date: string): string {
    let fecha_ = new Date(date)
    let dia =  fecha_.getDate()
    let mes = ""
    if (fecha_.getMonth() < 9){
       mes = "0" + (fecha_.getMonth() + 1)
    }else {
       mes = (fecha_.getMonth() + 1) + ''
    }
    let anio = fecha_.getFullYear()

    let fechaFormateada = `${dia}-${mes}-${anio}`

    return fechaFormateada
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://localhost:44308/api/Salidas/Obtener';

    this.http.get<SalidasResponse>(url).subscribe(
      (response) => {
        if (Array.isArray(response.data)) {
          this.arregloDatos = response.data;
        }
      },
      (error) => {
        console.error('Error al obtener datos:', error);
      }
    );
  }

  addSalida() {
    this.postSalida();
  }
  postSalida() {

    let url = 'https://localhost:44308/api/Salidas/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloSalida, { headers }).subscribe(
      (response) => {
        alert('Registro Exitoso');
        this.obtenerDatos()
      },
      (error) => {
        // Maneja los errores de la solicitud
        console.log(error)
        alert('Error: ' + error);
      }
    );
    this.limpiarModelo()
  }

  limpiarModelo(){

    this.modeloSalida.idSalida = 0
    this.modeloSalida.idEmpleado = 0
    this.modeloSalida.idCuarto = 0
    this.modeloSalida.idProducto = 0
    this.modeloSalida.cantidad = 0
    this.modeloSalida.tipo = ""
    this.modeloSalida.fecha = ""
  }

}

class Salidas{
  idSalida: number = 0;
  idProducto: number = 0;
  idCuarto: number = 0;
  idEmpleado: number = 0;
  fecha: string = "";
  cantidad: number = 0;
  tipo: string = ""

}

class SalidasResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
