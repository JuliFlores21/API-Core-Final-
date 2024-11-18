namespace API_Core_Final.Models
{
    public class RecomendacionDTO
    {
        public int IdRecomendacion { get; set; }
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public int IdActividad { get; set; }
        public string NombreActividad { get; set; }
        public decimal PesoAsignado { get; set; }
        public DateTime FechaRecomendacion { get; set; }
        public string NivelCondicionFisica { get; set; }
        public string PreferenciasDeporte { get; set; }
        public string ObjetivosPersonal { get; set; }
        public string RestriccionesFisicas { get; set; }
    }
}
