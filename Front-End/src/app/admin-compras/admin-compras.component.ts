import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-admin-compras',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-compras.component.html',
  styleUrl: './admin-compras.component.css'
})
export class AdminComprasComponent {

  modeloCompras: Compras = new Compras()
  arregloDatos: Compras [] = []
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
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Compras/Obtener';

    this.http.get<ComprasResponse>(url).subscribe(
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

  addCompra() {
    if (this.modeloCompras.idCompra == 0){
      this.postCompras()
    }else{
      this.editarCompra()
    }

  }

  editarCompra() {
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Compras/Editar'
    this.http.put(url, this.modeloCompras).subscribe(response => {
      alert('Registro Exitoso');
      this.obtenerDatos()
      this.limpiarModelo()
    }, error => {
      console.log(error)
      alert('Error: ' + error);
    });
  }
  postCompras() {

    let url = ''

    url = 'http://www.alimentoscarnisimasa.somee.com/api/Compras/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloCompras, { headers }).subscribe(
      (response) => {
        alert('Registro Exitoso');

        this.obtenerDatos()
        this.limpiarModelo()

      },
      (error) => {
        // Maneja los errores de la solicitud
        console.log(error)
        alert('Error: ' + error);
      }
    );
  }

  eliminarCompra(id : number){

    const urlDelete = 'http://www.alimentoscarnisimasa.somee.com/api/Compras/Eliminar';
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



  cargarDatos(item : any){
    this.modeloCompras = {

      idCompra : item.idCompra,
      idProveedor : item.idProveedor,
      idProducto : item.idProducto,
      cantidad : item.cantidad,
      total : item.total,
      precio : item.precio,
      fecha : item.fecha

    }
  }

  limpiarModelo(){
    this.modeloCompras.idCompra = 0
    this.modeloCompras.idProveedor = null
    this.modeloCompras.idProducto = null
    this.modeloCompras.cantidad = null
    this.modeloCompras.total = 0
    this.modeloCompras.precio = null
    this.modeloCompras.fecha = ""
  }

}

class Compras{
  idCompra: number = 0;
  idProveedor: number = null;
  idProducto: number = null;
  precio: number = null;
  fecha : string = "";
  cantidad: number = null;
  total: number = 0
}

class ComprasResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
