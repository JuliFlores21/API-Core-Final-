using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Core_Final.Models
{
    public class DetalleRecomendacion
    {
        [Key]
        public int IdDetalle { get; set; }

        [Required]
        public int IdRecomendacion { get; set; } // Clave foránea de Recomendacion

        [Required]
        [StringLength(100)]
        public string Parametro { get; set; } // Nombre del parámetro evaluado

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Porcentaje { get; set; } // Porcentaje de cada parámetro en la recomendación

        // Relación con Recomendacion
        [ForeignKey("IdRecomendacion")]
        public Recomendacion Recomendacion { get; set; }
    }
}
