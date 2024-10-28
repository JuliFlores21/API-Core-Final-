using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Core_Final.Models
{
    public class Estudiante
    {
        [Column("idEstudiante")]
        public int Id { get; set; }  // Mapea 'Id' a 'IdEstudiante' en la base de datos

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("NombreEstudiante")]
        public string Nombre { get; set; }  // Mapea 'Nombre' a 'NombreEstudiante'

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Column("CorreoEstudiante")]
        public string Correo { get; set; }  // Mapea 'Correo' a 'CorreoEstudiante'

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 dígitos.")]
        public string Cedula { get; set; }

        [Required]
        public string NivelCondicionFisica { get; set; }

        public string PreferenciasDeporte { get; set; }
        public string ObjetivosPersonales { get; set; }
        public string RestriccionesFisicas { get; set; }
        public int IdAdministrador { get; set; }
    }
}
