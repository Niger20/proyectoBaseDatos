import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {SoloLetrasDirective} from "../soloLetras/solo-letras.directive";

@Component({
  selector: 'app-admin-empleados',
  standalone: true,
  imports: [CommonModule, FormsModule, SoloLetrasDirective],
  templateUrl: './admin-empleados.component.html',
  styleUrl: './admin-empleados.component.css'
})
export class AdminEmpleadosComponent {

  modeloEmpleados: Empleados =  new Empleados()
  arregloDatos: Empleados [] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://localhost:44308/api/Empleados/Obtener';

    this.http.get<EmpleadosResponse>(url).subscribe(
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

  addEmpleado() {
    if (this.modeloEmpleados.idEmpleado == 0){
      this.postEmpleado()
    }else{
      this.editarEmpleado()
    }
  }

  editarEmpleado() {
    const url = 'https://localhost:44308/api/Empleados/Editar'
    this.http.put(url, this.modeloEmpleados).subscribe(response => {
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

    url = 'https://localhost:44308/api/Empleados/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloEmpleados, { headers }).subscribe(
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

    const urlDelete = 'https://localhost:44308/api/Empleados/Eliminar';
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
    this.modeloEmpleados.idEmpleado = 0
    this.modeloEmpleados.identidad = ""
    this.modeloEmpleados.primerNombre = ""
    this.modeloEmpleados.segundoNombre = ""
    this.modeloEmpleados.primerApellido = ""
    this.modeloEmpleados.segundoApellido = ""
  }

  cargarDatos(item : any){
    this.modeloEmpleados = {

      idEmpleado : item.idEmpleado,
      identidad : item.identidad,
      primerNombre : item.primerNombre,
      segundoNombre : item.segundoNombre,
      primerApellido : item.primerApellido,
      segundoApellido : item.segundoApellido,

    }
  }

}

class Empleados {
  idEmpleado: number = 0
  identidad: string = ""
  primerNombre: string = ""
  segundoNombre: string = ""
  segundoApellido: string = ""
  primerApellido: string = ""

}

class EmpleadosResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}
