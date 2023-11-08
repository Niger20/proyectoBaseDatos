using Microsoft.EntityFrameworkCore;
using PruebaProyecto.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//config CORS

builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyPolicy",
            policy =>
            {
                policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
            });
    }
);

//METODO NECESARIO A ANADIR PARA SELECCIONAR LA CADENA DE CONEXION
builder.Services.AddDbContext<MyDBcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
