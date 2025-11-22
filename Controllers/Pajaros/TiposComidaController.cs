using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDespliegue.Controllers.Pajaros
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposComidaController : ControllerBase
    {
        private readonly IMongoTipoComidaRepositoryService _tipoComidaService;

        public TiposComidaController(IMongoTipoComidaRepositoryService tipoComidaService)
        {
            _tipoComidaService = tipoComidaService;
        }

        // GET: api/tiposcomida
        [HttpGet]
        public async Task<ActionResult<List<TipoComida>>> GetAll()
        {
            var tipos = await _tipoComidaService.GetAllAsync();
            return Ok(tipos);
        }

        // GET: api/tiposcomida/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoComida>> GetById(string id)
        {
            var tipo = await _tipoComidaService.GetByIdAsync(id);
            if (tipo == null)
                return NotFound();
            return Ok(tipo);
        }

        // POST: api/tiposcomida
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TipoComida tipo)
        {
            await _tipoComidaService.CreateAsync(tipo);
            return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
        }

        // PUT: api/tiposcomida/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] TipoComida tipo)
        {
            if (id != tipo.Id)
                return BadRequest("El id no coincide");

            await _tipoComidaService.UpdateAsync(tipo);
            return NoContent();
        }

        // DELETE: api/tiposcomida/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _tipoComidaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
