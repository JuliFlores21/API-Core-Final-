using API_Core_Final.Data;
using API_Core_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Core_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActividadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Actividad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetActividades()
        {
            return await _context.Actividades.ToListAsync();
        }

        // GET: api/Actividad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actividad>> GetActividad(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return actividad;
        }

        // PUT: api/Actividad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisponibilidad(int id, [FromBody] bool disponibilidad)
        {
            // Buscar la actividad por ID
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad == null)
            {
                return NotFound("La actividad no fue encontrada.");
            }

            // Actualizar solo la disponibilidad
            actividad.Disponibilidad = disponibilidad;

            _context.Entry(actividad).Property(a => a.Disponibilidad).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool ActividadExists(int id)
        {
            return _context.Actividades.Any(e => e.IdActividad == id);
        }
    }
}
