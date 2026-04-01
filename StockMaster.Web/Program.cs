using Microsoft.EntityFrameworkCore;
using StockMaster.Data;
using StockMaster.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Usamos la cadena de conexión optimizada para puertos de nube (6543)
// Agregamos No Enclave para mayor compatibilidad
var connectionString = "Host=db.qfkvvqotreprdkgqzdpy.supabase.co;Port=6543;Database=postgres;Username=postgres;Password=y4fMQfZbmAUAegBq;SslMode=Require;Trust Server Certificate=true;Pooling=false;Timeout=60;Command Timeout=60";

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
