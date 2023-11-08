using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Empleados>().HasKey(e => e.IdEmpleado);
            modelBuilder.Entity<CuartosFrios>().HasKey(c => c.IdCuarto);
        }
    }
}
