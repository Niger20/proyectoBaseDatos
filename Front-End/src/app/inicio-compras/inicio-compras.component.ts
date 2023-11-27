import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReporteComprasComponent} from "../reporte-compras/reporte-compras.component";
import {ComprasComprasComponent} from "../compras-compras/compras-compras.component";
import {ComprasProductosComponent} from "../compras-productos/compras-productos.component";
import {ComprasProveedoresComponent} from "../compras-proveedores/compras-proveedores.component";
import {SharedServiceService} from "../services/shared-service.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-inicio-compras',
  standalone: true,
  imports: [CommonModule, ReporteComprasComponent, ComprasComprasComponent, ComprasProductosComponent, ComprasProveedoresComponent],
  templateUrl: './inicio-compras.component.html',
  styleUrl: './inicio-compras.component.css'
})
export class InicioComprasComponent {


  constructor(private sharedService: SharedServiceService, private http: HttpClient) {}

  cerrarSesion(){
    this.sharedService.logout()
  }

  pestanaActiva : string = 'inicio'

  cambiarPestana(pestana : string){
    this.pestanaActiva = pestana
  }
}
