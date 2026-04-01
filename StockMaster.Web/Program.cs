using Microsoft.EntityFrameworkCore;
using StockMaster.Data;
using StockMaster.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Usamos puerto 5432 (Directo) en lugar de 6543 (Pooler) para evitar demoras en local
var connectionString = "Host=aws-1-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.qfkvvqotreprdkgqzdpy;Password=y4fMQfZbmAUAegBq;SslMode=Require;Trust Server Certificate=true;Timeout=30;";

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ProductoService>();
builder.Services.AddControllers();

var app = builder.Build();

// Prueba de conexión inmediata al iniciar
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try {
        Console.WriteLine("--- Probando conexión con Supabase... ---");
        if (db.Database.CanConnect()) Console.WriteLine(">>> ¡CONEXIÓN EXITOSA CON SUPABASE! <<<");
        else Console.WriteLine(">>> ERROR: No se pudo conectar a la DB. Verifica tu proyecto en Supabase. <<<");
    } catch (Exception ex) {
        Console.WriteLine(">>> ERROR CRÍTICO: " + ex.Message);
    }
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();