import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";

@Component({
  selector: 'app-admin-productos',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective],
  templateUrl: './admin-productos.component.html',
  styleUrl: './admin-productos.component.css'
})
export class AdminProductosComponent {

  modeloProductos: Productos = new Productos()
  arregloDatos: Productos [] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Productos/Obtener';

    this.http.get<ProductosResponse>(url).subscribe(
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

  addProducto() {
    if (this.modeloProductos.idProducto == 0){
      this.postProductos()
    }else{
      this.editarProductos()
    }
  }

  editarProductos() {
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Productos/Editar'
    this.http.put(url, this.modeloProductos).subscribe(response => {
      alert('Registro Exitoso');
      this.obtenerDatos()
      this.limpiarModelo()
    }, error => {
      console.log(error)
      alert('Error: ' + error);
    });
  }
  postProductos() {

    let url = ''

    url = 'https://www.alimentoscarnisimasa.somee.com/api/Productos/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloProductos, { headers }).subscribe(
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

  eliminarProducto(id : number){

    const urlDelete = 'https://www.alimentoscarnisimasa.somee.com/api/Productos/Eliminar';
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

  limpiarModelo(){
    this.modeloProductos.idProducto = 0
    this.modeloProductos.cantidad = null
    this.modeloProductos.descripcion = ""
    this.modeloProductos.peso = null

  }

  cargarDatos(item : any){
    this.modeloProductos = {

      idProducto : item.idProducto,
      peso : item.peso,
      cantidad : item.cantidad,
      descripcion : item.descripcion,

    }
  }


}

class ProductosResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}

class Productos{
  idProducto: number = 0
  cantidad: number = 0
  descripcion: string = ""
  peso: number = null
}
