using Microsoft.EntityFrameworkCore;
using StockMaster.Data;
using StockMaster.Negocio;

var builder = WebApplication.CreateBuilder(args);

// CONEXIÓN ROBUSTA CON PUERTO 6543 Y POOLING OFF (Para evitar Timeouts de red)
var connectionString = "Host=db.qfkvvqotreprdkgqzdpy.supabase.co;Port=6543;Database=postgres;Username=postgres;Password=y4fMQfZbmAUAegBq;SslMode=Require;Trust Server Certificate=true;Pooling=false;Timeout=60";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ProductoService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
