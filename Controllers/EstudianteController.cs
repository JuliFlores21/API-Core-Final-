using API_Core_Final.Data;
using API_Core_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Core_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstudianteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estudiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        // GET: api/Estudiante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return estudiante;
        }

        // POST: api/Estudiante
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Envía todos los errores de validación al frontend
                var errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { message = "Errores de validación", errores });
            }

            // Validación de cédula ecuatoriana (10 dígitos y comienza con "17" para Pichincha)
            if (!ValidarCedulaEcuatoriana(estudiante.Cedula))
            {
                return BadRequest(new { message = "La cédula debe ser ecuatoriana, tener 10 dígitos y comenzar con 17 para la provincia de Pichincha." });
            }

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
        }

        // PUT: api/Estudiante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return BadRequest(new { message = "El ID proporcionado no coincide con el ID del estudiante." });
            }

            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Envía todos los errores de validación al frontend
                var errores = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { message = "Errores de validación", errores });
            }

            // Validación de cédula
            if (!ValidarCedulaEcuatoriana(estudiante.Cedula))
            {
                return BadRequest(new { message = "La cédula debe ser ecuatoriana, tener 10 dígitos y comenzar con 17." });
            }

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    return NotFound(new { message = "El estudiante no existe." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método para validar la cédula ecuatoriana de Pichincha (10 dígitos y comienza con "17")
        private bool ValidarCedulaEcuatoriana(string cedula)
        {
            // Verificar que tenga exactamente 10 dígitos y comience con "17" (Pichincha)
            if (cedula.Length != 10 || !cedula.StartsWith("17"))
                return false;

            int total = 0;
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int verificador = int.Parse(cedula[9].ToString());

            for (int i = 0; i < cedula.Length - 1; i++)
            {
                int valor = int.Parse(cedula[i].ToString()) * coeficientes[i];
                total += valor > 9 ? valor - 9 : valor;
            }

            int digitoCalculado = 10 - (total % 10);
            if (digitoCalculado == 10) digitoCalculado = 0;

            // Verifica si el dígito verificador coincide con el cálculo
            return digitoCalculado == verificador;
        }
        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }

    }
}
