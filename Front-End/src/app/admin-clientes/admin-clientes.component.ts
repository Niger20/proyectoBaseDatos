import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";
import {SoloNumeroDirective} from "../soloNumero/solo-numero.directive";

@Component({
  selector: 'app-admin-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective, SoloNumeroDirective],
  templateUrl: './admin-clientes.component.html',
  styleUrl: './admin-clientes.component.css'
})
export class AdminClientesComponent {

  modeloClientes: Clientes =  new Clientes()
  arregloDatos: Clientes [] = []

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Clientes/Obtener';

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
    if (this.modeloClientes.idCliente == 0){
      this.postProductos()
    }else{
      this.editarProductos()
    }
  }

  editarProductos() {
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Clientes/Editar'
    this.http.put(url, this.modeloClientes).subscribe(response => {
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

    url = 'https://www.alimentoscarnisimasa.somee.com/api/Clientes/Crear'

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

  eliminarCliente(id : number){

    const urlDelete = 'https://www.alimentoscarnisimasa.somee.com/api/Clientes/Eliminar';
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
    this.modeloClientes.idCliente = 0
    this.modeloClientes.identidad = ""
    this.modeloClientes.primerNombre = ""
    this.modeloClientes.segundoNombre = ""
    this.modeloClientes.primerApellido = ""
    this.modeloClientes.segundoApellido = ""
    this.modeloClientes.telefono = ""
  }

  cargarDatos(item : any){
    this.modeloClientes = {

      idCliente : item.idCliente,
      identidad : item.identidad,
      primerNombre : item.primerNombre,
      segundoNombre : item.segundoNombre,
      primerApellido : item.primerApellido,
      segundoApellido : item.segundoApellido,
      telefono : item.telefono,

    }
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
