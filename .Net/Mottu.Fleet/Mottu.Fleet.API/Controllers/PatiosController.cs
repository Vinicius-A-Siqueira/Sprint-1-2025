using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Services;
using Mottu.Fleet.Application.Interfaces;

namespace Mottu.Fleet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PatiosController : ControllerBase
{
    private readonly IPatioService _patioService;

    public PatiosController(IPatioService patioService)
    {
        _patioService = patioService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<PatioDto>>> GetPatios(int page = 1, int pageSize = 10, string? search = null)
    {
        var patios = await _patioService.GetPatiosAsync(page, pageSize, search);
        AddPaginationLinks(patios, page, pageSize, search);
        return Ok(patios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatioDto>> GetPatio(int id)
    {
        var patio = await _patioService.GetPatioByIdAsync(id);
        if (patio == null) return NotFound($"Pátio com ID {id} não encontrado.");
        AddPatioLinks(patio);
        return Ok(patio);
    }

    [HttpGet("{id:int}/details")]
    public async Task<ActionResult<PatioDto>> GetPatioWithMotos(int id)
    {
        var patio = await _patioService.GetPatioWithMotosAsync(id);
        if (patio == null) return NotFound($"Pátio com ID {id} não encontrado.");
        AddPatioLinks(patio);
        return Ok(patio);
    }

    [HttpPost]
    public async Task<ActionResult<PatioDto>> CreatePatio([FromBody] CreatePatioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var patio = await _patioService.CreatePatioAsync(dto);
            AddPatioLinks(patio);
            return CreatedAtAction(nameof(GetPatio), new { id = patio.Id }, patio);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatioDto>> UpdatePatio(int id, [FromBody] UpdatePatioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var patio = await _patioService.UpdatePatioAsync(id, dto);
            if (patio == null) return NotFound($"Pátio com ID {id} não encontrado.");
            AddPatioLinks(patio);
            return Ok(patio);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePatio(int id)
    {
        try
        {
            var success = await _patioService.DeletePatioAsync(id);
            if (!success) return NotFound($"Pátio com ID {id} não encontrado.");
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("occupancy-report")]
    public async Task<ActionResult<IEnumerable<PatioDto>>> GetOccupancyReport()
    {
        var patios = await _patioService.GetOccupancyReportAsync();
        foreach (var patio in patios)
            AddPatioLinks(patio);
        return Ok(patios);
    }

    private void AddPatioLinks(PatioDto patio)
    {
        patio.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetPatio), new { id = patio.Id })! },
            { "details", Url.Action(nameof(GetPatioWithMotos), new { id = patio.Id })! },
            { "update", Url.Action(nameof(UpdatePatio), new { id = patio.Id })! },
            { "delete", Url.Action(nameof(DeletePatio), new { id = patio.Id })! },
            { "all", Url.Action(nameof(GetPatios))! },
        };
    }

    private void AddPaginationLinks(PagedResultDto<PatioDto> result, int page, int pageSize, string? search)
    {
        result.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetPatios), new { page, pageSize, search })! }
        };
        if (result.HasPrevious)
            result.Links.Add("previous", Url.Action(nameof(GetPatios), new { page = page - 1, pageSize, search })!);
        if (result.HasNext)
            result.Links.Add("next", Url.Action(nameof(GetPatios), new { page = page + 1, pageSize, search })!);
    }
}
