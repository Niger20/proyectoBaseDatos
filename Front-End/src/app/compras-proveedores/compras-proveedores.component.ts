import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";

@Component({
  selector: 'app-compras-proveedores',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective],
  templateUrl: './compras-proveedores.component.html',
  styleUrl: './compras-proveedores.component.css'
})
export class ComprasProveedoresComponent {


  modeloProveedores: Proveedores =  new Proveedores()
  arregloDatos: Proveedores [] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://localhost:44308/api/Proveedor/Obtener';

    this.http.get<ProveedoresResponse>(url).subscribe(
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

  addProveedor() {
      this.postEmpleado()
  }
  postEmpleado() {

    let url = ''

    url = 'https://localhost:44308/api/Proveedor/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloProveedores, { headers }).subscribe(
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
    this.modeloProveedores.idProveedor = 0
    this.modeloProveedores.identidad = ""
    this.modeloProveedores.primerNombre = ""
    this.modeloProveedores.segundoNombre = ""
    this.modeloProveedores.primerApellido = ""
    this.modeloProveedores.segundoApellido = ""
  }
}


class Proveedores{
  idProveedor: number = 0
  identidad: string = ""
  primerNombre: string = ""
  segundoNombre: string = ""
  segundoApellido: string = ""
  primerApellido: string = ""
}

class ProveedoresResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
