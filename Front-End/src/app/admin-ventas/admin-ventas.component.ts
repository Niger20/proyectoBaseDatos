import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule, NgForm} from "@angular/forms";
@Component({
  selector: 'app-admin-ventas',
  standalone: true,
    imports: [CommonModule, FormsModule],
  templateUrl: './admin-ventas.component.html',
  styleUrl: './admin-ventas.component.css'
})
export class AdminVentasComponent {

  arregloDatos: Ventas [] = [];

  modeloVentas : Ventas = new Ventas()
  modeloSalidaExterna : SalidaExterna = new SalidaExterna()

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


  addVenta() {
    if (this.modeloVentas.idVenta == 0){
      this.postVentas()
    }else{
      this.editarVenta()
    }
  }

  editarVenta() {
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Ventas/Editar'
    this.http.put(url, this.modeloVentas).subscribe(response => {
      alert('Registro Exitoso');
      this.obtenerDatos()
      this.limpiarModelo()
    }, error => {
      console.log(error)
      alert('Error: ' + error);
    });
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Ventas/Obtener';

    this.http.get<VentasResponse>(url).subscribe(
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

  postVentas() {

    let url = ''

    url = 'http://www.alimentoscarnisimasa.somee.com/api/Ventas/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloVentas, { headers }).subscribe(
      (response) => {
        alert('Registro Exitoso');

        this.agregarSalidaExterna()

      },
      (error) => {
        // Maneja los errores de la solicitud
        console.log(error)
        alert('Error: ' + error);
      }
    );
  }


  eliminarVenta(id : number){

    const urlDelete = 'http://www.alimentoscarnisimasa.somee.com/api/Ventas/Eliminar';
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

  agregarSalidaExterna(){

    const url = 'http://www.alimentoscarnisimasa.somee.com/api/SalidaExterna/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    this.modeloSalidaExterna.idVenta = (this.arregloDatos[this.arregloDatos.length - 1].idVenta) + 1;

    // Realiza la solicitud POST
    this.http.post(url, this.modeloSalidaExterna, { headers }).subscribe(
      (response) => {
        this.obtenerDatos()
      },
      (error) => {
        // Maneja los errores de la solicitud
        console.log(error)
        alert('Error: ' + error);
      }
    );
    this.limpiarModelo()
    this.limpiarModeloSalidaExterna()
  }

  cargarDatos(item : any){
    this.modeloVentas = {

      idVenta : item.idVenta,
      idCliente : item.idCliente,
      idProducto : item.idProducto,
      cantidad : item.cantidad,
      total : item.total,
      precio : item.precio,
      fecha : item.fecha

    }
  }

  limpiarModelo(){
      this.modeloVentas.idVenta = 0
      this.modeloVentas.idCliente = null
      this.modeloVentas.idProducto = null
      this.modeloVentas.cantidad = null
      this.modeloVentas.total = 0
      this.modeloVentas.precio = null
      this.modeloVentas.fecha = ""
  }

  limpiarModeloSalidaExterna(){
    this.modeloSalidaExterna.idSalidaExterna = 0
    this.modeloSalidaExterna.idSalida = null
    this.modeloSalidaExterna.idVenta = 0
  }

}

class Ventas{
  idVenta: number = 0;
  idCliente: number = null;
  idProducto: number = null;
  precio: number = null;
  fecha : string = "";
  cantidad: number = null;
  total: number = 0
}

class VentasResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}

class SalidaExterna {
  idSalidaExterna: number = 0;
  idSalida: number = null;
  idVenta: number = 0;
}
