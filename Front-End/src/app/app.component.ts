
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {LoginComponent} from "./login/login.component"
import {EntradasComponent} from "./entradas/entradas.component";
import {SalidasComponent} from "./salidas/salidas.component";
import {HttpClient, HttpClientModule, HttpHeaders} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {SharedServiceService} from "./services/shared-service.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, LoginComponent, HttpClientModule, SalidasComponent, EntradasComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Front-End'
  loginStatus : boolean = false
  userRol : string = ""

  constructor(private sharedService: SharedServiceService) {}

  ngOnInit(): void {

    this.sharedService.getUserRole().subscribe((rol) => {
      this.userRol = rol;
    });

    this.sharedService.isLoggedIn.subscribe((loggedIn) => {
      this.loginStatus = loggedIn;
    });



  }

  mostrarRol(){
    console.log(this.userRol)
  }
}
