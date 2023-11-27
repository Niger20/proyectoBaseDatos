import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule, NgForm} from "@angular/forms";

@Component({
  selector: 'app-bodegueros-entradas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bodegueros-entradas.component.html',
  styleUrl: './bodegueros-entradas.component.css'
})
export class BodeguerosEntradasComponent {

  arregloDatos: Entradas [] = [];
  modeloEntradas: Entradas = new Entradas()
  modeloMovimientoInterno : MovimientoInterno = new MovimientoInterno()
  modeloEntradaExterna : EntradaExterna = new EntradaExterna()


  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  addSalida() {
      this.postEntrada()
  }


  postEntrada() {

    let url = ''

    url = 'http://www.alimentoscarnisimasa.somee.com/api/Entradas/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloEntradas, { headers }).subscribe(
      (response) => {
        alert('Registro Exitoso');
        if (this.modeloEntradas.tipo === 'interna'){

          this.agregarMovimientoInterno()

        }else{

          this.agregarEntradaExterna()
        }

      },
      (error) => {
        // Maneja los errores de la solicitud
        console.log(error)
        alert('Error: ' + error);
      }
    );
  }

  agregarMovimientoInterno(){

    const url = 'http://www.alimentoscarnisimasa.somee.com/api/MovimientoInterno/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    this.modeloMovimientoInterno.idEntrada = (this.arregloDatos[this.arregloDatos.length - 1].idEntrada) + 1;

    // Realiza la solicitud POST
    this.http.post(url, this.modeloMovimientoInterno, { headers }).subscribe(
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
    this.limpiarModeloMovimientoInterno()
    this.limpiarModeloEntradaExterna()
  }

  agregarEntradaExterna(){

    const url = 'http://www.alimentoscarnisimasa.somee.com/api/EntradaExterna/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });



    this.modeloEntradaExterna.idEntrada = (this.arregloDatos[this.arregloDatos.length - 1].idEntrada) + 1;

    // Realiza la solicitud POST
    this.http.post(url, this.modeloEntradaExterna, { headers }).subscribe(
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
    this.limpiarModeloMovimientoInterno()
    this.limpiarModeloEntradaExterna()
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
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Entradas/Obtener';

    this.http.get<EntradasResponse>(url).subscribe(
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
  limpiarModelo(){

    this.modeloEntradas.idEntrada = 0
    this.modeloEntradas.idEmpleado = null
    this.modeloEntradas.idCuarto = null
    this.modeloEntradas.idProducto = null
    this.modeloEntradas.cantidad = null
    this.modeloEntradas.tipo = ""
    this.modeloEntradas.fecha = ""
  }

  limpiarModeloEntradaExterna(){

    this.modeloEntradaExterna.idCompra = null
    this.modeloEntradaExterna.idEntrada = null

  }

  limpiarModeloMovimientoInterno(){

    this.modeloMovimientoInterno.idSalida = null
    this.modeloMovimientoInterno.idEntrada = null

  }

}

class Entradas{

  idEntrada: number = 0;
  idProducto: number = null;
  idCuarto: number = null;
  idEmpleado: number = null;
  fecha: string = "";
  cantidad: number = null;
  tipo: string = ""

}

class MovimientoInterno{
  idMovimientoInterno: number = 0;
  idEntrada: number = null;
  idSalida: number = null;
}

class EntradaExterna{
  idEntrada: number = null;
  idCompra: number = null;
}

class EntradasResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
