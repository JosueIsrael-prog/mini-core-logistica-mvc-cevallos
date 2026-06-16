namespace Logistica.API.DTOs;

public class ReporteRepartidorDto
{
    public string NombreRepartidor { get; set; } = string.Empty;
    public int CantidadEnvios { get; set; }
    public decimal TotalKg { get; set; }
    public string Zona { get; set; } = string.Empty;
    public decimal TarifaAplicada { get; set; }
    public decimal CostoTotal { get; set; }
}
