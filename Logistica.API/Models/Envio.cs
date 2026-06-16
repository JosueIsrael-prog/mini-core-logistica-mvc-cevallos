using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistica.API.Models;

[Table("envios")]
public class Envio
{
    [Key]
    [Column("id_envio")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEnvio { get; set; }

    [Required]
    [Column("id_repartidor")]
    public int IdRepartidor { get; set; }

    [Required]
    [Column("id_zona")]
    public int IdZona { get; set; }

    [Required]
    [Column("peso_kg", TypeName = "decimal(10,2)")]
    public decimal PesoKg { get; set; }

    [Required]
    [Column("fecha_envio")]
    public DateTime FechaEnvio { get; set; }

    // Navegación
    [ForeignKey(nameof(IdRepartidor))]
    public Repartidor Repartidor { get; set; } = null!;

    [ForeignKey(nameof(IdZona))]
    public Zona Zona { get; set; } = null!;
}
