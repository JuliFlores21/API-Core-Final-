using API_Core_Final.Data;
using API_Core_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Core_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecomendacionController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para obtener la lista de recomendaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecomendacionDTO>>> GetRecomendaciones()
        {
            var recomendaciones = await _context.Recomendaciones
                .Include(r => r.Estudiante)
                .Include(r => r.Actividad)
                .Select(r => new RecomendacionDTO
                {
                    IdRecomendacion = r.IdRecomendacion,
                    IdEstudiante = r.IdEstudiante,
                    NombreEstudiante = r.Estudiante.Nombre,
                    IdActividad = r.IdActividad,
                    NombreActividad = r.Actividad.NombreActividad,
                    PesoAsignado = r.PesoAsignado,
                    FechaRecomendacion = r.FechaRecomendacion
                })
                .ToListAsync();

            return Ok(recomendaciones);
        }

        // Nuevo endpoint para obtener una recomendación específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RecomendacionDTO>> GetRecomendacion(int id)
        {
            var recomendacion = await _context.Recomendaciones
                .Include(r => r.Estudiante)
                .Include(r => r.Actividad)
                .Where(r => r.IdRecomendacion == id)
                .Select(r => new RecomendacionDTO
                {
                    IdRecomendacion = r.IdRecomendacion,
                    IdEstudiante = r.IdEstudiante,
                    NombreEstudiante = r.Estudiante.Nombre,
                    IdActividad = r.IdActividad,
                    NombreActividad = r.Actividad.NombreActividad,
                    PesoAsignado = r.PesoAsignado,
                    FechaRecomendacion = r.FechaRecomendacion,

                    // Información adicional del estudiante
                    NivelCondicionFisica = r.Estudiante.NivelCondicionFisica,
                    PreferenciasDeporte = r.Estudiante.PreferenciasDeporte,
                    ObjetivosPersonal = r.Estudiante.ObjetivosPersonales,
                    RestriccionesFisicas = r.Estudiante.RestriccionesFisicas
                })
                .FirstOrDefaultAsync();

            if (recomendacion == null)
            {
                return NotFound("No se encontró la recomendación especificada.");
            }

            return Ok(recomendacion);
        }


        // Endpoint para obtener los detalles de una recomendación específica
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<IEnumerable<DetalleRecomendacionDTO>>> GetDetallesRecomendacion(int id)
        {
            var detalles = await _context.DetallesRecomendacion
                .Where(d => d.IdRecomendacion == id)
                .Select(d => new DetalleRecomendacionDTO
                {
                    IdDetalle = d.IdDetalle,
                    Parametro = d.Parametro,
                    Porcentaje = d.Porcentaje
                })
                .ToListAsync();

            if (detalles == null || detalles.Count == 0)
            {
                return NotFound("No se encontraron detalles para la recomendación especificada.");
            }

            return Ok(detalles);
        }

        private bool RecomendacionExists(int id)
        {
            return _context.Recomendaciones.Any(e => e.IdRecomendacion == id);
        }
    }
}
