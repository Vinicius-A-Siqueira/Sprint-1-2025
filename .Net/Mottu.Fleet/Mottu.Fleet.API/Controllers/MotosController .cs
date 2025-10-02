using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Fleet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MotosController : ControllerBase
{
    private readonly IMotoService _motoService;

    public MotosController(IMotoService motoService)
    {
        _motoService = motoService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<MotoDto>>> GetMotos(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] int? patioId = null,
        [FromQuery] MotoStatus? status = null)
    {
        var result = await _motoService.GetMotosAsync(page, pageSize, search, patioId, status);
        AddPaginationLinks(result, page, pageSize, search, patioId, status);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MotoDto>> GetMoto(int id)
    {
        var moto = await _motoService.GetMotoByIdAsync(id);
        if (moto == null)
            return NotFound($"Moto com ID {id} não encontrada.");

        AddMotoLinks(moto);
        return Ok(moto);
    }

    [HttpGet("placa/{placa}")]
    public async Task<ActionResult<MotoDto>> GetMotoByPlaca(string placa)
    {
        var moto = await _motoService.GetMotoByPlacaAsync(placa);
        if (moto == null)
            return NotFound($"Moto com placa '{placa}' não encontrada.");

        AddMotoLinks(moto);
        return Ok(moto);
    }

    [HttpGet("patio/{patioId}")]
    public async Task<ActionResult<IEnumerable<MotoDto>>> GetMotosByPatio(int patioId)
    {
        var motos = await _motoService.GetMotosByPatioAsync(patioId);
        return Ok(motos);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<MotoDto>>> GetMotosByStatus(MotoStatus status)
    {
        var motos = await _motoService.GetMotosByStatusAsync(status);
        return Ok(motos);
    }

    [HttpPost]
    public async Task<ActionResult<MotoDto>> CreateMoto([FromBody] CreateMotoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var moto = await _motoService.CreateMotoAsync(dto);
            AddMotoLinks(moto);
            return CreatedAtAction(nameof(GetMoto), new { id = moto.Id }, moto);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MotoDto>> UpdateMoto(int id, [FromBody] UpdateMotoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var moto = await _motoService.UpdateMotoAsync(id, dto);
            if (moto == null)
                return NotFound($"Moto com ID {id} não encontrada.");

            AddMotoLinks(moto);
            return Ok(moto);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMoto(int id)
    {
        var success = await _motoService.DeleteMotoAsync(id);
        if (!success)
            return NotFound($"Moto com ID {id} não encontrada.");

        return NoContent();
    }

    [HttpPut("{id:int}/move")]
    public async Task<IActionResult> MoveMotoToPatio(int id, [FromBody] int newPatioId)
    {
        var success = await _motoService.MoveMotoToPatioAsync(id, newPatioId);
        if (!success)
            return NotFound($"Moto com ID {id} não encontrada.");

        return NoContent();
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateMotoStatus(int id, [FromBody] MotoStatus newStatus)
    {
        var success = await _motoService.UpdateMotoStatusAsync(id, newStatus);
        if (!success)
            return NotFound($"Moto com ID {id} não encontrada.");

        return NoContent();
    }

    [HttpPut("{id:int}/maintenance")]
    public async Task<IActionResult> RegisterMaintenance(int id, [FromBody] MaintenanceRequest request)
    {
        var success = await _motoService.RegisterMaintenanceAsync(id, request.MaintenanceDate, request.Observations);
        if (!success)
            return NotFound($"Moto com ID {id} não encontrada.");

        return NoContent();
    }

    // Corrigir inicialização dos links
    private void AddMotoLinks(MotoDto moto)
    {
        moto.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetMoto), new { id = moto.Id }) ?? string.Empty },
            { "update", Url.Action(nameof(UpdateMoto), new { id = moto.Id }) ?? string.Empty },
            { "delete", Url.Action(nameof(DeleteMoto), new { id = moto.Id }) ?? string.Empty },
            { "move", Url.Action(nameof(MoveMotoToPatio), new { id = moto.Id }) ?? string.Empty },
            { "status", Url.Action(nameof(UpdateMotoStatus), new { id = moto.Id }) ?? string.Empty },
            { "maintenance", Url.Action(nameof(RegisterMaintenance), new { id = moto.Id }) ?? string.Empty }
        };
    }

    // Implementação exemplo de AddPaginationLinks para paginar resultados
    private void AddPaginationLinks(PagedResultDto<MotoDto> result, int page, int pageSize, string? search, int? patioId, MotoStatus? status)
    {
        result.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetMotos), new { page, pageSize, search, patioId, status }) ?? string.Empty }
        };

        if (result.CurrentPage > 1)
            result.Links.Add("previous", Url.Action(nameof(GetMotos), new { page = page - 1, pageSize, search, patioId, status }) ?? string.Empty);

        if (result.CurrentPage < result.TotalPages)
            result.Links.Add("next", Url.Action(nameof(GetMotos), new { page = page + 1, pageSize, search, patioId, status }) ?? string.Empty);
    }
}

public class MaintenanceRequest
{
    public DateTime MaintenanceDate { get; set; }
    public string? Observations { get; set; }
}
