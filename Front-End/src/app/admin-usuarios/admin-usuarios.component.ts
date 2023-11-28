import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-admin-usuarios',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './admin-usuarios.component.html',
  styleUrl: './admin-usuarios.component.css'
})
export class AdminUsuariosComponent {

  modeloUsuarios: Usuarios = new Usuarios()
  arregloDatos: Usuarios [] = []
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.obtenerDatos();
  }

  obtenerDatos() {

    this.arregloDatos = []
    const url = 'https://www.alimentoscarnisimasa.somee.com/api/Usuarios/Obtener';

    this.http.get<UsuariosResponse>(url).subscribe(
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

  addUsuario() {
      this.postUsuarios()
  }
  postUsuarios() {

    let url = ''

    url = 'https://www.alimentoscarnisimasa.somee.com/api/Usuarios/Crear'

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    // Realiza la solicitud POST
    this.http.post(url, this.modeloUsuarios, { headers }).subscribe(
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

  eliminarUsuario(username : string){

    const urlDelete = 'https://www.alimentoscarnisimasa.somee.com/api/Usuarios/Eliminar';
    // Realiza la solicitud POST
    this.http.delete(`${urlDelete}?username=${username}`).subscribe(
      (response) => {

        alert('usuario eliminado Exitosamente')
        this.obtenerDatos()
      },
      (error) => {
        // Maneja los errores de la solicitud
        alert('Error: '+ JSON.stringify(error));
      }
    );

  }

  limpiarModelo(){
    this.modeloUsuarios.username = ""
    this.modeloUsuarios.password = ""
    this.modeloUsuarios.rol = ""

  }


}

class UsuariosResponse{
  code: string = "";
  message: string = ""
  data: string ="";
}

class Usuarios{
  username: string = ""
  password: string = ""
  rol: string = ""
}
