import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {HttpHeaders} from "@angular/common/http";
import {SharedServiceService} from "../services/shared-service.service";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private sharedService: SharedServiceService, private http: HttpClient) {}

  modeloUsuario: Usuario = new Usuario()
  modeloLogin: LoginResponse = new LoginResponse()

  loginUsuario() {

    let url = "https://localhost:44308/api/Usuarios/Login"


    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    this.http.post(url, this.modeloUsuario, { headers }).subscribe(
      (response : any) => {

        this.modeloLogin.username = response.username
        this.modeloLogin.rol = response.rol
        this.modeloLogin.loginStatus = response.loginStatus

        if (this.modeloLogin.loginStatus === 'valido'){
          this.sharedService.login()
          this.sharedService.setUserRole(this.modeloLogin.rol)
        }else{
          this.sharedService.logout()
        }


      },
      (error) => {
        // Maneja los errores de la solicitud
        alert('Error: '+ error);
      }
    );
  }

}
export class Usuario {
  username: string= "";
  password: string = "";
  rol: string = ""
}

export class LoginResponse {

  username: string = "";
  loginStatus: string = "";
  rol: string = ""

}


