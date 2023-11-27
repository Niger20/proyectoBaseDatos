import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AdminSalidasComponent} from "../admin-salidas/admin-salidas.component";
import {AdminEntradasComponent} from "../admin-entradas/admin-entradas.component";
import {AdminVentasComponent} from "../admin-ventas/admin-ventas.component";
import {AdminComprasComponent} from "../admin-compras/admin-compras.component";
import {AdminClientesComponent} from "../admin-clientes/admin-clientes.component";
import {AdminProveedoresComponent} from "../admin-proveedores/admin-proveedores.component";
import {AdminEmpleadosComponent} from "../admin-empleados/admin-empleados.component";
import {AdminUsuariosComponent} from "../admin-usuarios/admin-usuarios.component";
import {AdminProductosComponent} from "../admin-productos/admin-productos.component";
import {SharedServiceService} from "../services/shared-service.service";
import {HttpClient} from "@angular/common/http";
import {ReporteVentasComponent} from "../reporte-ventas/reporte-ventas.component";
import {ReporteComprasComponent} from "../reporte-compras/reporte-compras.component";
import {ReporteCuartosComponent} from "../reporte-cuartos/reporte-cuartos.component";

@Component({
  selector: 'app-inicio-admin',
  standalone: true,
  imports: [CommonModule, AdminSalidasComponent, AdminEntradasComponent, AdminVentasComponent, AdminComprasComponent, AdminClientesComponent, AdminProveedoresComponent, AdminEmpleadosComponent, AdminUsuariosComponent, AdminProductosComponent, ReporteVentasComponent, ReporteComprasComponent, ReporteCuartosComponent],
  templateUrl: './inicio-admin.component.html',
  styleUrl: './inicio-admin.component.css'
})
export class InicioAdminComponent {

  constructor(private sharedService: SharedServiceService, private http: HttpClient) {}

  cerrarSesion(){
    this.sharedService.logout()
  }

  pestanaActiva:string = "inicio"
  reporteActivo : string = 'ventas'

  cambiarReporte(reporte:string){
    this.reporteActivo = reporte
  }
  cambiarPestana(pestana : string){
    this.pestanaActiva = pestana
  }

}
