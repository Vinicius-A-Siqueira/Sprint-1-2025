using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Repositories;

namespace Mottu.Fleet.API.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Produces("application/json")]
    public class PatiosMongoController : ControllerBase
    {
        private readonly PatioMongoRepository _repository;

        public PatiosMongoController(PatioMongoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna todos os pátios
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PatioMongo>>> GetAll()
        {
            var patios = await _repository.GetAllAsync();
            return Ok(patios);
        }

        /// <summary>
        /// Retorna um pátio por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatioMongo>> GetById(string id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { message = "Pátio não encontrado." });

            return Ok(patio);
        }

        /// <summary>
        /// Retorna apenas pátios ativos
        /// </summary>
        [HttpGet("ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PatioMongo>>> GetAtivos()
        {
            var patios = await _repository.GetAtivosAsync();
            return Ok(patios);
        }

        /// <summary>
        /// Retorna pátios com vagas disponíveis
        /// </summary>
        [HttpGet("disponiveis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PatioMongo>>> GetDisponiveis()
        {
            var patios = await _repository.GetComVagasDisponiveisAsync();
            return Ok(patios);
        }

        /// <summary>
        /// Cria um novo pátio
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PatioMongo>> Create([FromBody] PatioMongo patio)
        {
            if (patio.Capacidade <= 0)
                return BadRequest(new { message = "Capacidade deve ser maior que zero." });

            var novoPatio = await _repository.CreateAsync(patio);
            return CreatedAtAction(nameof(GetById), new { id = novoPatio.Id }, novoPatio);
        }

        /// <summary>
        /// Atualiza um pátio existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(string id, [FromBody] PatioMongo patio)
        {
            var existingPatio = await _repository.GetByIdAsync(id);
            if (existingPatio == null)
                return NotFound(new { message = "Pátio não encontrado." });

            patio.Id = id;
            patio.DataCadastro = existingPatio.DataCadastro;
            patio.VagasOcupadas = existingPatio.VagasOcupadas;

            var updated = await _repository.UpdateAsync(id, patio);
            if (!updated)
                return StatusCode(500, new { message = "Erro ao atualizar pátio." });

            return NoContent();
        }

        /// <summary>
        /// Desativa um pátio (soft delete)
        /// </summary>
        [HttpPatch("{id}/desativar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Desativar(string id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { message = "Pátio não encontrado." });

            var desativado = await _repository.DesativarAsync(id);
            if (!desativado)
                return StatusCode(500, new { message = "Erro ao desativar pátio." });

            return NoContent();
        }

        /// <summary>
        /// Deleta um pátio permanentemente
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(string id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { message = "Pátio não encontrado." });

            // Verificar se há motos no pátio
            if (patio.VagasOcupadas > 0)
                return BadRequest(new { message = "Não é possível deletar pátio com motos." });

            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return StatusCode(500, new { message = "Erro ao deletar pátio." });

            return NoContent();
        }
    }
}
