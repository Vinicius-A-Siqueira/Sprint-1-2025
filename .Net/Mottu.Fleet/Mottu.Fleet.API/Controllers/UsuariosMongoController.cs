using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Repositories;

namespace Mottu.Fleet.API.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Produces("application/json")]
    public class UsuariosMongoController : ControllerBase
    {
        private readonly UsuarioMongoRepository _repository;

        public UsuariosMongoController(UsuarioMongoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UsuarioMongo>>> GetAll()
        {
            var usuarios = await _repository.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna um usuário por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioMongo>> GetById(string id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        /// <summary>
        /// Retorna um usuário por username
        /// </summary>
        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioMongo>> GetByUsername(string username)
        {
            var usuario = await _repository.GetByUsernameAsync(username);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        /// <summary>
        /// Retorna usuários por perfil
        /// </summary>
        [HttpGet("perfil/{perfil}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UsuarioMongo>>> GetByPerfil(string perfil)
        {
            var usuarios = await _repository.GetByPerfilAsync(perfil);
            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna apenas usuários ativos
        /// </summary>
        [HttpGet("ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UsuarioMongo>>> GetAtivos()
        {
            var usuarios = await _repository.GetAtivosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioMongo>> Create([FromBody] UsuarioMongo usuario)
        {
            // Validar se username já existe
            if (await _repository.ExistsByUsernameAsync(usuario.Username))
                return BadRequest(new { message = $"Username {usuario.Username} já está em uso." });

            // Validar se email já existe
            if (!string.IsNullOrEmpty(usuario.Email) && await _repository.ExistsByEmailAsync(usuario.Email))
                return BadRequest(new { message = $"Email {usuario.Email} já está em uso." });

            var novoUsuario = await _repository.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = novoUsuario.Id }, novoUsuario);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(string id, [FromBody] UsuarioMongo usuario)
        {
            var existingUsuario = await _repository.GetByIdAsync(id);
            if (existingUsuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            usuario.Id = id;
            usuario.DataCadastro = existingUsuario.DataCadastro;

            var updated = await _repository.UpdateAsync(id, usuario);
            if (!updated)
                return StatusCode(500, new { message = "Erro ao atualizar usuário." });

            return NoContent();
        }

        /// <summary>
        /// Atualiza o último acesso do usuário
        /// </summary>
        [HttpPatch("{id}/ultimo-acesso")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AtualizarUltimoAcesso(string id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            await _repository.AtualizarUltimoAcessoAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Desativa um usuário (soft delete)
        /// </summary>
        [HttpPatch("{id}/desativar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Desativar(string id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var desativado = await _repository.DesativarAsync(id);
            if (!desativado)
                return StatusCode(500, new { message = "Erro ao desativar usuário." });

            return NoContent();
        }

        /// <summary>
        /// Deleta um usuário permanentemente
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return StatusCode(500, new { message = "Erro ao deletar usuário." });

            return NoContent();
        }
    }
}
