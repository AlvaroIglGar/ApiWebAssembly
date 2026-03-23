using ApiRestDespliegue.Interfaces;
using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDespliegue.Controllers.Pajaros
{
    [Route("api/[controller]")]
    [ApiController]
    public class PajarosController : ControllerBase
    {
        private readonly IMongoHistoricoPajaroRepositoryService _pajarosHistoricoRepos;
        private readonly IMongoPajaroRepositoryService _pajarosRepos;
        private readonly IConfiguration _config;

        public PajarosController(
            IMongoHistoricoPajaroRepositoryService pajarosHistoricoRepos,
            IMongoPajaroRepositoryService pajarosRepos,
            IConfiguration config)
        {
            _pajarosHistoricoRepos = pajarosHistoricoRepos;
            _pajarosRepos = pajarosRepos;
            _config = config;
        }

        // --------------------- PAJAROS ---------------------

        // GET: api/pajaros
        // Obtiene todos los pájaros registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pajaro>>> GetAllPajaros()
        {
            var pajaros = await _pajarosRepos.GetAllAsync();
            return Ok(pajaros);
        }

        // POST: api/pajaros
        // Crea un nuevo pájaro
        [HttpPost]
        public async Task<ActionResult> CreatePajaro([FromBody] Pajaro pajaro)
        {
            await _pajarosRepos.CreateAsync(pajaro);
            return CreatedAtAction(nameof(GetAllPajaros), new { id = pajaro.Id }, pajaro);
        }

        // PUT: api/pajaros/{id}
        // Actualiza un pájaro existente
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePajaro(string id, [FromBody] Pajaro pajaro)
        {
            if (id != pajaro.Id)
                return BadRequest("El id del pájaro no coincide");

            await _pajarosRepos.UpdateAsync(pajaro);
            return NoContent();
        }

        // DELETE: api/pajaros/{id}
        // Elimina un pájaro por Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePajaro(string id)
        {
            await _pajarosRepos.DeleteAsync(id);
            return NoContent();
        }

        // --------------------- HISTORICO PAJAROS ---------------------

        // GET: api/pajaros/{id}/historico
        // Obtiene todo el histórico de un pájaro concreto
        [HttpGet("{id}/historico")]
        public async Task<ActionResult<IEnumerable<HistoricoPajaro>>> GetHistoricoByPajaroId(string id)
        {
            var historico = await _pajarosHistoricoRepos.GetByPajaroIdAsync(id);

            if (historico == null)
                return NotFound();

            return Ok(historico);
        }

        // GET: api/pajaros/{id}/historico/rango?fechaInicio=2026-03-01&fechaFin=2026-03-31
        // Obtiene el histórico de un pájaro filtrado por un rango de fechas
        // Muy útil para la pantalla mensual de histórico, gráficas y resumen
        [HttpGet("{id}/historico-por-rango")]
        public async Task<ActionResult<IEnumerable<HistoricoPajaro>>> GetHistoricoByPajaroIdAndRango(
           string id,
           [FromQuery] DateTime fechaInicio,
           [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
                return BadRequest("Debes indicar fechaInicio y fechaFin válidas.");

            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha fin.");

            var historico = await _pajarosHistoricoRepos.GetByPajaroIdAndDateRangeAsync(id, fechaInicio, fechaFin);

            return Ok(historico);
        }

        // GET: api/pajaros/historico/{id}
        // Obtiene un único registro histórico por su Id
        // Sirve para consultar o editar un registro concreto
        [HttpGet("historico/{id}")]
        public async Task<ActionResult<HistoricoPajaro>> GetHistoricoById(string id)
        {
            var historico = await _pajarosHistoricoRepos.GetByIdAsync(id);

            if (historico == null)
                return NotFound($"No se encontró ningún registro histórico con Id {id}");

            return Ok(historico);
        }

        // POST: api/pajaros/historico
        // Crea un nuevo registro histórico de alimentación / actividad
        [HttpPost("historico")]
        public async Task<ActionResult> CreateHistorico([FromBody] HistoricoPajaro registro)
        {
            await _pajarosHistoricoRepos.CreateAsync(registro);
            return CreatedAtAction(nameof(GetHistoricoByPajaroId), new { id = registro.PajaroId }, registro);
        }

        // PUT: api/pajaros/historico/{id}
        // Actualiza un registro histórico existente
        [HttpPut("historico/{id}")]
        public async Task<ActionResult> UpdateHistorico(string id, [FromBody] HistoricoPajaro registro)
        {
            if (registro == null)
                return BadRequest("El registro histórico es obligatorio.");

            if (string.IsNullOrWhiteSpace(registro.Id))
                registro.Id = id;

            if (id != registro.Id)
                return BadRequest("El id del registro histórico no coincide");

            var existente = await _pajarosHistoricoRepos.GetByIdAsync(id);
            if (existente == null)
                return NotFound($"No se encontró ningún registro histórico con Id {id}");

            await _pajarosHistoricoRepos.UpdateAsync(registro);
            return NoContent();
        }

        // DELETE: api/pajaros/historico/{id}
        // Elimina un registro histórico por su Id
        [HttpDelete("historico/{id}")]
        public async Task<ActionResult> DeleteHistorico(string id)
        {
            var existente = await _pajarosHistoricoRepos.GetByIdAsync(id);
            if (existente == null)
                return NotFound($"No se encontró ningún registro histórico con Id {id}");

            await _pajarosHistoricoRepos.DeleteAsync(id);
            return NoContent();
        }
    }
}