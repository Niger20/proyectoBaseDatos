import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {BodeguerosEntradasComponent} from "../bodegueros-entradas/bodegueros-entradas.component";
import {BodeguerosSalidasComponent} from "../bodegueros-salidas/bodegueros-salidas.component";
import {ReporteCuartosComponent} from "../reporte-cuartos/reporte-cuartos.component";
import {SharedServiceService} from "../services/shared-service.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-inicio-bodeguero',
  standalone: true,
  imports: [CommonModule, BodeguerosEntradasComponent, BodeguerosSalidasComponent, ReporteCuartosComponent],
  templateUrl: './inicio-bodeguero.component.html',
  styleUrl: './inicio-bodeguero.component.css'
})
export class InicioBodegueroComponent {

  constructor(private sharedService: SharedServiceService, private http: HttpClient) {}

  cerrarSesion(){
    this.sharedService.logout()
  }

  pestanaActiva:string = "inicio"

  cambiarPestana(pestana : string){
    this.pestanaActiva = pestana
  }

}
