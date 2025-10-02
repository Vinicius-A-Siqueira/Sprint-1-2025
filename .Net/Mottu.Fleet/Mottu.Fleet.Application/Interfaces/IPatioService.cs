using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Interfaces;

public interface IPatioService
{
    Task<PatioDto> CreatePatioAsync(CreatePatioDto dto);
    Task<PatioDto?> GetPatioByIdAsync(int id);
    Task<PatioDto?> GetPatioWithMotosAsync(int id);
    Task<bool> DeletePatioAsync(int id);
    Task<PatioDto?> UpdatePatioAsync(int id, UpdatePatioDto dto);
    Task<IEnumerable<PatioDto>> GetOccupancyReportAsync();
    Task<PagedResultDto<PatioDto>> GetPatiosAsync(int page = 1, int pageSize = 10, string? search = null);
}

