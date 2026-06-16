using Microsoft.EntityFrameworkCore;
using Logistica.API.Data;

var builder = WebApplication.CreateBuilder(args);

// ──────────────── Servicios ────────────────

// Registrar DbContext con PostgreSQL (Supabase)
builder.Services.AddDbContext<LogisticaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ──────────────── Aplicar migraciones y seed data ────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogisticaDbContext>();
    db.Database.Migrate();
}

// ──────────────── Pipeline HTTP ────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { status = "Logistica API running", timestamp = DateTime.UtcNow }))
   .WithName("HealthCheck")
   .WithOpenApi();

app.Run();
