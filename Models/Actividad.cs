using System.ComponentModel.DataAnnotations;

namespace API_Core_Final.Models
{
    public class Actividad
    {
        [Key]
        public int IdActividad { get; set; } // Mapeo del idActividad como clave primaria

        [Required]
        [StringLength(100)]
        public string NombreActividad { get; set; } // Nombre de la actividad

        [Required]
        public int CapacidadMaxima { get; set; } // Capacidad máxima de participantes en la actividad

        [Required]
        public bool Disponibilidad { get; set; } // Disponibilidad de la actividad (1 = disponible, 0 = no disponible)

        [StringLength(255)]
        public string RequerimientosFisicos { get; set; }
    }
}
