using Microsoft.EntityFrameworkCore;
using PruebaProyecto.Controllers;
using PruebaProyecto.Models;


namespace PruebaProyecto.DAL
{
    public class MyDBcontext : DbContext
    {
        public MyDBcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        
        public DbSet<CuartosFrios> CuartosFrios { get; set; }
        
        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<Proveedores> Proveedores { get; set; }
        
        public DbSet<Compras> Compras { get; set; }
        
        public DbSet<Usuarios> Usuarios { get; set; }
        
        public DbSet<Ventas> Ventas { get; set; }
        
        public DbSet<VistaVentas> VistaVentas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Empleados>().HasKey(e => e.IdEmpleado);
            modelBuilder.Entity<CuartosFrios>().HasKey(c => c.IdCuarto);
            modelBuilder.Entity<Clientes>().HasKey(cli => cli.IdCliente);

            modelBuilder.Entity<Proveedores>().HasKey(pro => pro.IdProveedor);
            modelBuilder.Entity<Compras>().HasKey(com => com.IdCompra);

            modelBuilder.Entity<Usuarios>().HasKey(u => u.Username);

            modelBuilder.Entity<Ventas>().HasKey(v => v.IdVenta);

            modelBuilder.Entity<VistaVentas>().HasNoKey();


        }
    }
}
