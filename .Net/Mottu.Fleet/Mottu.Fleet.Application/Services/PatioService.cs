using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;

namespace Mottu.Fleet.Application.Services;

public class PatioService : IPatioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatioService(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<PatioDto> CreatePatioAsync(CreatePatioDto dto)
    {
        var patio = _mapper.Map<Patio>(dto);
        await _unitOfWork.Patios.AddAsync(patio);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PatioDto>(patio);
    }

    public async Task<PatioDto?> GetPatioByIdAsync(int id)
    {
        var patio = await _unitOfWork.Patios.GetByIdAsync(id);
        return patio is null ? null : _mapper.Map<PatioDto>(patio);
    }

    public async Task<PatioDto?> GetPatioWithMotosAsync(int id)
    {
        var patio = await _unitOfWork.Patios.GetByIdWithMotosAsync(id);
        return patio is null ? null : _mapper.Map<PatioDto>(patio);
    }

    public async Task<PagedResultDto<PatioDto>> GetPatiosAsync(int page = 1, int pageSize = 10, string? search = null)
    {
        var patios = await _unitOfWork.Patios.GetAllAsync(page, pageSize);
        int total = await _unitOfWork.Patios.CountAsync();

        var dtos = _mapper.Map<IEnumerable<PatioDto>>(patios);
        var result = new PagedResultDto<PatioDto>
        {
            Items = dtos,
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };

        return result;
    }

    public async Task<bool> DeletePatioAsync(int id)
    {
        var patio = await _unitOfWork.Patios.GetByIdWithMotosAsync(id);
        if (patio == null)
            return false;

        if (patio.Motos.Count > 0)
            throw new InvalidOperationException("Pátio possui motos e não pode ser removido.");

        await _unitOfWork.Patios.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PatioDto>> GetOccupancyReportAsync()
    {
        var patios = await _unitOfWork.Patios.GetAllAsync(1, int.MaxValue);

        var list = patios != null
            ? patios.Select(p => _mapper.Map<PatioDto>(p)).ToList()
            : new List<PatioDto>();

        foreach (var patioDto in list)
        {
            var patioEntity = patios!.FirstOrDefault(p => p.Id == patioDto.Id);
            var totalMotos = patioEntity?.Motos.Count ?? 0;
            patioDto.QuantidadeMotos = totalMotos;
            patioDto.TaxaOcupacao = patioDto.Capacidade > 0 ? (decimal)totalMotos / patioDto.Capacidade * 100 : 0;
        }

        return list;
    }

    public async Task<PatioDto?> UpdatePatioAsync(int id, UpdatePatioDto dto)
    {
        var patio = await _unitOfWork.Patios.GetByIdAsync(id);
        if (patio == null) return null;

        _mapper.Map(dto, patio);
        await _unitOfWork.Patios.UpdateAsync(patio);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PatioDto>(patio);
    }
}
