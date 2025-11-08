using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Repositories;

namespace Mottu.Fleet.API.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Produces("application/json")]
    public class MotosMongoController : ControllerBase
    {
        private readonly MotoMongoRepository _motoRepository;
        private readonly PatioMongoRepository _patioRepository;

        public MotosMongoController(
            MotoMongoRepository motoRepository,
            PatioMongoRepository patioRepository)
        {
            _motoRepository = motoRepository;
            _patioRepository = patioRepository;
        }

        /// <summary>
        /// Retorna todas as motos cadastradas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MotoMongo>>> GetAll()
        {
            var motos = await _motoRepository.GetAllAsync();
            return Ok(motos);
        }

        /// <summary>
        /// Retorna uma moto específica por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoMongo>> GetById(string id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                return NotFound(new { message = "Moto não encontrada." });

            return Ok(moto);
        }

        /// <summary>
        /// Retorna uma moto por placa
        /// </summary>
        [HttpGet("placa/{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoMongo>> GetByPlaca(string placa)
        {
            var moto = await _motoRepository.GetByPlacaAsync(placa);
            if (moto == null)
                return NotFound(new { message = $"Moto com placa {placa} não encontrada." });

            return Ok(moto);
        }

        /// <summary>
        /// Retorna todas as motos de um pátio específico
        /// </summary>
        [HttpGet("patio/{patioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MotoMongo>>> GetByPatio(string patioId)
        {
            var motos = await _motoRepository.GetByPatioIdAsync(patioId);
            return Ok(motos);
        }

        /// <summary>
        /// Retorna motos por status
        /// </summary>
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MotoMongo>>> GetByStatus(string status)
        {
            var motos = await _motoRepository.GetByStatusAsync(status);
            return Ok(motos);
        }

        /// <summary>
        /// Cria uma nova moto
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoMongo>> Create([FromBody] MotoMongo moto)
        {
            // Validar se placa já existe
            if (await _motoRepository.ExistsByPlacaAsync(moto.Placa))
                return BadRequest(new { message = $"Já existe uma moto com a placa {moto.Placa}." });

            // Validar se pátio existe (se informado)
            if (!string.IsNullOrEmpty(moto.PatioId))
            {
                var patio = await _patioRepository.GetByIdAsync(moto.PatioId);
                if (patio == null)
                    return BadRequest(new { message = "Pátio não encontrado." });

                // Verificar capacidade
                if (patio.VagasOcupadas >= patio.Capacidade)
                    return BadRequest(new { message = "Pátio sem vagas disponíveis." });

                // Incrementar vagas ocupadas
                await _patioRepository.IncrementarVagasOcupadasAsync(moto.PatioId);
            }

            var novaMoto = await _motoRepository.CreateAsync(moto);
            return CreatedAtAction(nameof(GetById), new { id = novaMoto.Id }, novaMoto);
        }

        /// <summary>
        /// Atualiza uma moto existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(string id, [FromBody] MotoMongo moto)
        {
            var existingMoto = await _motoRepository.GetByIdAsync(id);
            if (existingMoto == null)
                return NotFound(new { message = "Moto não encontrada." });

            // Se mudou de pátio, atualizar vagas
            if (existingMoto.PatioId != moto.PatioId)
            {
                // Decrementar no pátio antigo
                if (!string.IsNullOrEmpty(existingMoto.PatioId))
                    await _patioRepository.DecrementarVagasOcupadasAsync(existingMoto.PatioId);

                // Incrementar no novo pátio
                if (!string.IsNullOrEmpty(moto.PatioId))
                {
                    var novoPatio = await _patioRepository.GetByIdAsync(moto.PatioId);
                    if (novoPatio == null)
                        return BadRequest(new { message = "Novo pátio não encontrado." });

                    if (novoPatio.VagasOcupadas >= novoPatio.Capacidade)
                        return BadRequest(new { message = "Novo pátio sem vagas disponíveis." });

                    await _patioRepository.IncrementarVagasOcupadasAsync(moto.PatioId);
                }
            }

            moto.Id = id;
            moto.DataCadastro = existingMoto.DataCadastro;

            var updated = await _motoRepository.UpdateAsync(id, moto);
            if (!updated)
                return StatusCode(500, new { message = "Erro ao atualizar moto." });

            return NoContent();
        }

        /// <summary>
        /// Deleta uma moto
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null)
                return NotFound(new { message = "Moto não encontrada." });

            // Se estava em um pátio, decrementar vagas
            if (!string.IsNullOrEmpty(moto.PatioId))
                await _patioRepository.DecrementarVagasOcupadasAsync(moto.PatioId);

            var deleted = await _motoRepository.DeleteAsync(id);
            if (!deleted)
                return StatusCode(500, new { message = "Erro ao deletar moto." });

            return NoContent();
        }
    }
}
