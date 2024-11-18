using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Core_Final.Models
{
    public class Recomendacion
    {
        [Key]
        public int IdRecomendacion { get; set; }

        [Required]
        public int IdEstudiante { get; set; } // Clave foránea de Estudiante

        [Required]
        public int IdActividad { get; set; } // Clave foránea de ActividadDeportiva

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PesoAsignado { get; set; } // Ponderación total de la recomendación

        [Required]
        public DateTime FechaRecomendacion { get; set; } // Fecha de la recomendación

        // Relaciones
        [ForeignKey("IdEstudiante")]
        public Estudiante Estudiante { get; set; }

        [ForeignKey("IdActividad")]
        public Actividad Actividad { get; set; }

        // Relación uno a muchos con DetalleRecomendacion
        public ICollection<DetalleRecomendacion> DetallesRecomendacion { get; set; }
    }
}
