import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";
import {SoloNumeroDirective} from "../soloNumero/solo-numero.directive";

@Component({
  selector: 'app-vendedor-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective, SoloNumeroDirective],
  templateUrl: './vendedor-clientes.component.html',
  styleUrl: './vendedor-clientes.component.css'
})
export class VendedorClientesComponent {


  modeloClientes: Clientes =  new Clientes()
  arregloDatos: Clientes [] = []

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'http://www.alimentoscarnisimasa.somee.com/api/Clientes/Obtener';

    this.http.get<ClientesResponse>(url).subscribe(
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

    url = 'http://www.alimentoscarnisimasa.somee.com/api/Clientes/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloClientes, { headers }).subscribe(
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
    this.modeloClientes.idCliente = 0
    this.modeloClientes.identidad = ""
    this.modeloClientes.primerNombre = ""
    this.modeloClientes.segundoNombre = ""
    this.modeloClientes.primerApellido = ""
    this.modeloClientes.segundoApellido = ""
    this.modeloClientes.telefono = ""
  }
}

class Clientes {
  idCliente: number = 0
  identidad: string = ""
  primerNombre: string = ""
  segundoNombre: string = ""
  segundoApellido: string = ""
  primerApellido: string = ""
  telefono: string = ""

}

class ClientesResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
