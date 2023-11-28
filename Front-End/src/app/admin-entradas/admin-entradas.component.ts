import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule, NgForm} from "@angular/forms";

@Component({
  selector: 'app-admin-entradas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-entradas.component.html',
  styleUrl: './admin-entradas.component.css'
})
export class AdminEntradasComponent {

  arregloDatos: Entradas [] = [];
  modeloEntradas: Entradas = new Entradas()
  modeloMovimientoInterno : MovimientoInterno = new MovimientoInterno()
  modeloEntradaExterna : EntradaExterna = new EntradaExterna()


  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  addSalida() {
    if (this.modeloEntradas.idEntrada == 0){
      this.postEntrada()
    }else{
      this.editarSalida()
    }

  }
  postEntrada() {

    let url = ''

    url = 'https://www.alimentoscarnisimasa.somee.com/api/Entradas/Crear'

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

    const url = 'https://www.alimentoscarnisimasa.somee.com/api/MovimientoInterno/Crear'

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

    const url = 'https://www.alimentoscarnisimasa.somee.com/api/EntradaExterna/Crear'

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

  eliminarEntrada(id : number){

    const urlDelete = 'https://www.alimentoscarnisimasa.somee.com/api/Entradas/Eliminar';
    // Realiza la solicitud POST
    this.http.delete(`${urlDelete}?id=${id}`).subscribe(
      (response) => {

        alert('Registro eliminado Exitosamente')
        this.obtenerDatos()
      },
      (error) => {
        // Maneja los errores de la solicitud
        alert('Error: '+ JSON.stringify(error));
      }
    );

  }
  obtenerDatos() {
    this.arregloDatos = []
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Entradas/Obtener';

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

  editarSalida() {
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Entradas/EditarEntradas'
    this.http.put(url, this.modeloEntradas).subscribe(response => {
      alert('Registro Exitoso');
      this.obtenerDatos()
      this.limpiarModelo()
    }, error => {
      console.log(error)
      alert('Error: ' + error);
    });
  }

  cargarDatos(item : any){
    this.modeloEntradas = {

      idEntrada : item.idEntrada,
      idEmpleado : item.idEmpleado,
      idCuarto : item.idCuarto,
      idProducto : item.idProducto,
      cantidad : item.cantidad,
      tipo : item.tipo,
      fecha : item.fecha

    }
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

