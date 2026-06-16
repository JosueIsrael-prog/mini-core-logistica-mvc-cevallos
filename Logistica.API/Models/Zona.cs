using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistica.API.Models;

[Table("zonas")]
public class Zona
{
    [Key]
    [Column("id_zona")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdZona { get; set; }

    [Required]
    [Column("nombre_zona")]
    [MaxLength(100)]
    public string NombreZona { get; set; } = string.Empty;

    [Required]
    [Column("tarifa_por_kg", TypeName = "decimal(10,2)")]
    public decimal TarifaPorKg { get; set; }

    // Navegación inversa
    public ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
