namespace Logistica.API.DTOs;

/// <summary>
/// DTO que estructura la respuesta del reporte logístico por repartidor,
/// desglosado por zona, según la rúbrica del proyecto.
/// </summary>
public class ReporteRepartidorDto
{
    public string NombreRepartidor { get; set; } = string.Empty;
    public int CantidadEnvios { get; set; }
    public decimal TotalKg { get; set; }
    public string Zona { get; set; } = string.Empty;
    public decimal TarifaAplicada { get; set; }
    public decimal CostoTotal { get; set; }
}
