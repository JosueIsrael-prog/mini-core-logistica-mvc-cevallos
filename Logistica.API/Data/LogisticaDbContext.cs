using Microsoft.EntityFrameworkCore;
using Logistica.API.Models;

namespace Logistica.API.Data;

public class LogisticaDbContext : DbContext
{
    public LogisticaDbContext(DbContextOptions<LogisticaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Repartidor> Repartidores => Set<Repartidor>();
    public DbSet<Zona> Zonas => Set<Zona>();
    public DbSet<Envio> Envios => Set<Envio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasOne(e => e.Repartidor)
                  .WithMany(r => r.Envios)
                  .HasForeignKey(e => e.IdRepartidor)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Zona)
                  .WithMany(z => z.Envios)
                  .HasForeignKey(e => e.IdZona)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed: Zonas
        modelBuilder.Entity<Zona>().HasData(
            new Zona { IdZona = 1, NombreZona = "Norte",  TarifaPorKg = 1.50m },
            new Zona { IdZona = 2, NombreZona = "Sur",    TarifaPorKg = 2.00m },
            new Zona { IdZona = 3, NombreZona = "Centro", TarifaPorKg = 1.75m }
        );

        // Seed: Repartidores
        modelBuilder.Entity<Repartidor>().HasData(
            new Repartidor { IdRepartidor = 1, Nombre = "Andrés", Email = "andres@logistica.com" },
            new Repartidor { IdRepartidor = 2, Nombre = "Camila", Email = "camila@logistica.com" },
            new Repartidor { IdRepartidor = 3, Nombre = "Luis",   Email = "luis@logistica.com" }
        );

        // Seed: Envíos
        modelBuilder.Entity<Envio>().HasData(
            new Envio { IdEnvio = 1, IdRepartidor = 1, IdZona = 1, PesoKg = 10.00m, FechaEnvio = new DateTime(2025, 5, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 2, IdRepartidor = 1, IdZona = 1, PesoKg = 12.00m, FechaEnvio = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 3, IdRepartidor = 1, IdZona = 1, PesoKg = 10.00m, FechaEnvio = new DateTime(2025, 5, 22, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 4, IdRepartidor = 2, IdZona = 2, PesoKg = 8.00m,  FechaEnvio = new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 5, IdRepartidor = 2, IdZona = 2, PesoKg = 10.00m, FechaEnvio = new DateTime(2025, 5, 18, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 6, IdRepartidor = 1, IdZona = 3, PesoKg = 5.50m,  FechaEnvio = new DateTime(2025, 5, 14, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 7, IdRepartidor = 2, IdZona = 1, PesoKg = 7.00m,  FechaEnvio = new DateTime(2025, 5, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 8, IdRepartidor = 3, IdZona = 1, PesoKg = 15.00m, FechaEnvio = new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 9, IdRepartidor = 1, IdZona = 2, PesoKg = 6.00m,  FechaEnvio = new DateTime(2025, 6, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Envio { IdEnvio = 10, IdRepartidor = 3, IdZona = 3, PesoKg = 9.00m, FechaEnvio = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
