import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";

@Component({
  selector: 'app-compras-productos',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective],
  templateUrl: './compras-productos.component.html',
  styleUrl: './compras-productos.component.css'
})
export class ComprasProductosComponent {



  modeloProductos: Productos = new Productos()
  arregloDatos: Productos [] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://localhost:44308/api/Productos/Obtener';

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
      this.postProductos()
  }


  postProductos() {

    let url = ''

    url = 'https://localhost:44308/api/Productos/Crear'

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
  limpiarModelo(){
    this.modeloProductos.idProducto = 0
    this.modeloProductos.cantidad = null
    this.modeloProductos.descripcion = ""
    this.modeloProductos.peso = null

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

