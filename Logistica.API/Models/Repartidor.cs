using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistica.API.Models;

[Table("repartidores")]
public class Repartidor
{
    [Key]
    [Column("id_repartidor")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRepartidor { get; set; }

    [Required]
    [Column("nombre")]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Column("email")]
    [MaxLength(150)]
    public string? Email { get; set; }

    // Navegación inversa
    public ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
