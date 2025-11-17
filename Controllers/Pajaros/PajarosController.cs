using ApiRestDespliegue.Interfaces;
using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using Microsoft.AspNetCore.Http;
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


        public PajarosController(IMongoHistoricoPajaroRepositoryService pajarosHistoricoRepos, IMongoPajaroRepositoryService pajarosRepos, IConfiguration config)
        {
            _pajarosHistoricoRepos = pajarosHistoricoRepos;
            _pajarosRepos = pajarosRepos;
            _config = config;
        }

        // --------------------- PAJAROS ---------------------

        // GET: api/pajaros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pajaro>>> GetAllPajaros()
        {
            var pajaros = await _pajarosRepos.GetAllAsync();
            return Ok(pajaros);
        }

        // POST: api/pajaros
        [HttpPost]
        public async Task<ActionResult> CreatePajaro([FromBody] Pajaro pajaro)
        {
            await _pajarosRepos.CreateAsync(pajaro);
            return CreatedAtAction(nameof(GetAllPajaros), new { id = pajaro.Id }, pajaro);
        }

        // PUT: api/pajaros/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePajaro(string id, [FromBody] Pajaro pajaro)
        {
            if (id != pajaro.Id)
                return BadRequest("El id del pájaro no coincide");

            await _pajarosRepos.UpdateAsync(pajaro);
            return NoContent();
        }

        // DELETE: api/pajaros/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePajaro(string id)
        {
            await _pajarosRepos.DeleteAsync(id);
            return NoContent();
        }

        // --------------------- HISTORICO PAJAROS ---------------------

        // GET: api/pajaros/{id}/historico
        [HttpGet("{id}/historico")]
        public async Task<ActionResult<IEnumerable<HistoricoPajaro>>> GetHistoricoByPajaroId(string id)
        {
            var historico = await _pajarosHistoricoRepos.GetByPajaroIdAsync(id);
            if (historico == null)
                return NotFound();
            return Ok(historico);
        }

        // POST: api/pajaros/historico
        [HttpPost("historico")]
        public async Task<ActionResult> CreateHistorico([FromBody] HistoricoPajaro registro)
        {
            await _pajarosHistoricoRepos.CreateAsync(registro);
            return CreatedAtAction(nameof(GetHistoricoByPajaroId), new { id = registro.PajaroId }, registro);
        }

        // PUT: api/pajaros/historico/{id}
        [HttpPut("historico/{id}")]
        public async Task<ActionResult> UpdateHistorico(string id, [FromBody] HistoricoPajaro registro)
        {
            if (id != registro.Id)
                return BadRequest("El id del registro histórico no coincide");

            await _pajarosHistoricoRepos.UpdateAsync(registro);
            return NoContent();
        }

        // DELETE: api/pajaros/historico/{id}
        [HttpDelete("historico/{id}")]
        public async Task<ActionResult> DeleteHistorico(string id)
        {
            await _pajarosHistoricoRepos.DeleteAsync(id);
            return NoContent();
        }

    }
}
