using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDespliegue.Controllers.Pajaros
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly IMongoActividadRepositoryService _ActividadService;

        public ActividadController(IMongoActividadRepositoryService ActividadService)
        {
            _ActividadService = ActividadService;
        }

        // GET: api/tiposcomida
        [HttpGet]
        public async Task<ActionResult<List<Actividad>>> GetAll()
        {
            var tipos = await _ActividadService.GetAllAsync();
            return Ok(tipos);
        }

        // GET: api/tiposcomida/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Actividad>> GetById(string id)
        {
            var tipo = await _ActividadService.GetByIdAsync(id);
            if (tipo == null)
                return NotFound();
            return Ok(tipo);
        }

        // POST: api/tiposcomida
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Actividad tipo)
        {
            await _ActividadService.CreateAsync(tipo);
            return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
        }

        // PUT: api/tiposcomida/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Actividad tipo)
        {
            if (id != tipo.Id)
                return BadRequest("El id no coincide");

            await _ActividadService.UpdateAsync(tipo);
            return NoContent();
        }

        // DELETE: api/tiposcomida/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _ActividadService.DeleteAsync(id);
            return NoContent();
        }
    }
}
