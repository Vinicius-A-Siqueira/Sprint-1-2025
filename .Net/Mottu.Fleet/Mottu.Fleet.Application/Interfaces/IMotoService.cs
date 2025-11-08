using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Interfaces;

public interface IMotoService
{
    Task<MotoDto> CreateMotoAsync(CreateMotoDto dto);
    Task<MotoDto?> GetMotoByIdAsync(int id);
    Task<MotoDto?> GetMotoByPlacaAsync(string placa);
    Task<IEnumerable<MotoDto>> GetMotosByPatioAsync(int patioId);
    Task<MotoDto?> UpdateMotoAsync(int id, UpdateMotoDto dto);
    Task<bool> DeleteMotoAsync(int id);
    Task<bool> MoveMotoToPatioAsync(int motoId, int newPatioId);
    Task<bool> RegisterMaintenanceAsync(int motoId, DateTime maintenanceDate, string? observations = null);
    Task<IEnumerable<MotoDto>> GetFleetReportAsync();
}
