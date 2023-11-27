import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AdminClientesComponent} from "../admin-clientes/admin-clientes.component";
import {AdminComprasComponent} from "../admin-compras/admin-compras.component";
import {AdminEmpleadosComponent} from "../admin-empleados/admin-empleados.component";
import {AdminEntradasComponent} from "../admin-entradas/admin-entradas.component";
import {AdminProductosComponent} from "../admin-productos/admin-productos.component";
import {AdminProveedoresComponent} from "../admin-proveedores/admin-proveedores.component";
import {AdminSalidasComponent} from "../admin-salidas/admin-salidas.component";
import {AdminUsuariosComponent} from "../admin-usuarios/admin-usuarios.component";
import {AdminVentasComponent} from "../admin-ventas/admin-ventas.component";
import {ReporteComprasComponent} from "../reporte-compras/reporte-compras.component";
import {ReporteCuartosComponent} from "../reporte-cuartos/reporte-cuartos.component";
import {ReporteVentasComponent} from "../reporte-ventas/reporte-ventas.component";
import {VendedorVentasComponent} from "../vendedor-ventas/vendedor-ventas.component";
import {VendedorClientesComponent} from "../vendedor-clientes/vendedor-clientes.component";
import {SharedServiceService} from "../services/shared-service.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-inicio-vendedor',
  standalone: true,
  imports: [CommonModule, AdminClientesComponent, AdminComprasComponent, AdminEmpleadosComponent, AdminEntradasComponent, AdminProductosComponent, AdminProveedoresComponent, AdminSalidasComponent, AdminUsuariosComponent, AdminVentasComponent, ReporteComprasComponent, ReporteCuartosComponent, ReporteVentasComponent, VendedorVentasComponent, VendedorClientesComponent],
  templateUrl: './inicio-vendedor.component.html',
  styleUrl: './inicio-vendedor.component.css'
})
export class InicioVendedorComponent {


  constructor(private sharedService: SharedServiceService, private http: HttpClient) {}

  cerrarSesion(){
    this.sharedService.logout()
  }

  pestanaActiva: string = "inicio"

  cambiarPestana (pestana:string){
    this.pestanaActiva = pestana
  }
}
