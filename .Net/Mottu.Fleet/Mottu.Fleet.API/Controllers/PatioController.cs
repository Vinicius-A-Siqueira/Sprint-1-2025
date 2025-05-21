using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;

namespace Mottu.Fleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : ControllerBase
    {
        private readonly IPatioService _patioService;

        public PatioController(IPatioService patioService)
        {
            _patioService = patioService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_patioService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patio = _patioService.GetById(id);
            if (patio == null) return NotFound();
            return Ok(patio);
        }

        [HttpPost]
        public IActionResult Create(PatioDTO patioDto)
        {
            _patioService.Create(patioDto);
            return Created("", patioDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PatioDTO patioDto)
        {
            _patioService.Update(id, patioDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _patioService.Delete(id);
            return NoContent();
        }
    }
}
