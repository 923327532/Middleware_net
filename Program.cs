using Semana09.Caso1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Middleware en línea para proteger zona /admin
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/admin"))
    {
        var key = context.Request.Query["key"].ToString();
        if (string.IsNullOrEmpty(key) || key != "secret123")
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Acceso denegado: clave requerida");
            return; // Cortocircuito - no llama a next()
        }
    }
    await next();
});

app.UseAuthorization();

app.MapStaticAssets();

// Middleware de logging de solicitudes (DEMO: mover antes/después de UseStaticAssets para ver diferencia)
app.UseRequestLog();

// Middleware terminal para /estado
app.Map("/estado", async context =>
{
    await context.Response.WriteAsync("Sistema operativo correctamente");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
