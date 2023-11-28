import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";


@Component({
  selector: 'app-admin-proveedores',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective],
  templateUrl: './admin-proveedores.component.html',
  styleUrl: './admin-proveedores.component.css'
})
export class AdminProveedoresComponent {

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
    if (this.modeloProveedores.idProveedor == 0){
      this.postEmpleado()
    }else{
      this.editarEmpleado()
    }
  }

  editarEmpleado() {
    const url = 'https://localhost:44308/api/Proveedor/Editar'
    this.http.put(url, this.modeloProveedores).subscribe(response => {
      alert('Registro Exitoso');
      this.obtenerDatos()
      this.limpiarModelo()
    }, error => {
      console.log(error)
      alert('Error: ' + error);
    });
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

  eliminarEmpleado(id : number){

    const urlDelete = 'https://localhost:44308/api/Proveedor/Eliminar';
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
    this.modeloProveedores.idProveedor = 0
    this.modeloProveedores.identidad = ""
    this.modeloProveedores.primerNombre = ""
    this.modeloProveedores.segundoNombre = ""
    this.modeloProveedores.primerApellido = ""
    this.modeloProveedores.segundoApellido = ""
  }

  cargarDatos(item : any){
    this.modeloProveedores = {

      idProveedor : item.idProveedor,
      identidad : item.identidad,
      primerNombre : item.primerNombre,
      segundoNombre : item.segundoNombre,
      primerApellido : item.primerApellido,
      segundoApellido : item.segundoApellido,

    }
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
