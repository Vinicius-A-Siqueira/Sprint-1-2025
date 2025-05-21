using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;

namespace Mottu.Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialController : ControllerBase
    {
        private readonly IFilialService _filialService;

        public FilialController(IFilialService filialService)
        {
            _filialService = filialService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_filialService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var filial = _filialService.GetById(id);
            if (filial == null) return NotFound();
            return Ok(filial);
        }

        [HttpPost]
        public IActionResult Create(FilialDTO filialDto)
        {
            _filialService.Create(filialDto);
            return Created("", filialDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FilialDTO filialDto)
        {
            _filialService.Update(id, filialDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _filialService.Delete(id);
            return NoContent();
        }
    }
}
