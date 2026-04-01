using Microsoft.EntityFrameworkCore;
using StockMaster.Data;
using StockMaster.Negocio;

var builder = WebApplication.CreateBuilder(args);

// CORRECCIÓN: Usamos el Host del Pooler y el Usuario con el ID del proyecto para que funcione en Render (IPv4)
var connectionString = "Host=aws-1-us-east-2.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.qfkvvqotreprdkgqzdpy;Password=y4fMQfZbmAUAegBq;SslMode=Require;Trust Server Certificate=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ProductoService>();
builder.Services.AddControllers();

var app = builder.Build();

// Render necesita saber que el puerto es el 8080 (que pusimos en el Dockerfile)
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();
