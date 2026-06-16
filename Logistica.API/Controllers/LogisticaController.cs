using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logistica.API.Data;
using Logistica.API.DTOs;

namespace Logistica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogisticaController : ControllerBase
{
    private readonly LogisticaDbContext _context;

    public LogisticaController(LogisticaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Genera el reporte logístico de todos los repartidores dentro de un rango de fechas.
    /// Cada repartidor se desglosa por zona; si no tiene envíos, aparece con valores en cero.
    /// </summary>
    /// <param name="fechaInicio">Fecha de inicio del rango (inclusive).</param>
    /// <param name="fechaFin">Fecha de fin del rango (inclusive).</param>
    [HttpGet("reporte")]
    public async Task<ActionResult<List<ReporteRepartidorDto>>> GetReporte(
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin)
    {
        // Normalizar fechas a UTC para compatibilidad con PostgreSQL timestamptz
        var inicio = DateTime.SpecifyKind(fechaInicio.Date, DateTimeKind.Utc);
        var fin = DateTime.SpecifyKind(fechaFin.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

        // Obtener todos los repartidores
        var repartidores = await _context.Repartidores
            .OrderBy(r => r.Nombre)
            .ToListAsync();

        // Obtener envíos dentro del rango con sus zonas (eager loading)
        var enviosEnRango = await _context.Envios
            .Include(e => e.Zona)
            .Where(e => e.FechaEnvio >= inicio && e.FechaEnvio <= fin)
            .ToListAsync();

        var resultado = new List<ReporteRepartidorDto>();

        foreach (var repartidor in repartidores)
        {
            // Filtrar envíos de este repartidor
            var enviosDelRepartidor = enviosEnRango
                .Where(e => e.IdRepartidor == repartidor.IdRepartidor)
                .ToList();

            if (enviosDelRepartidor.Count == 0)
            {
                // Repartidor sin envíos en el período → fila con valores en cero
                resultado.Add(new ReporteRepartidorDto
                {
                    NombreRepartidor = repartidor.Nombre,
                    CantidadEnvios = 0,
                    TotalKg = 0,
                    Zona = "—",
                    TarifaAplicada = 0,
                    CostoTotal = 0
                });
            }
            else
            {
                // Agrupar por zona para desglosar el cálculo
                var grupos = enviosDelRepartidor
                    .GroupBy(e => e.IdZona)
                    .OrderBy(g => g.First().Zona.NombreZona);

                foreach (var grupo in grupos)
                {
                    var zona = grupo.First().Zona;
                    var totalKg = grupo.Sum(e => e.PesoKg);
                    var costoTotal = totalKg * zona.TarifaPorKg;

                    resultado.Add(new ReporteRepartidorDto
                    {
                        NombreRepartidor = repartidor.Nombre,
                        CantidadEnvios = grupo.Count(),
                        TotalKg = totalKg,
                        Zona = zona.NombreZona,
                        TarifaAplicada = zona.TarifaPorKg,
                        CostoTotal = costoTotal
                    });
                }
            }
        }

        return Ok(resultado);
    }
}
