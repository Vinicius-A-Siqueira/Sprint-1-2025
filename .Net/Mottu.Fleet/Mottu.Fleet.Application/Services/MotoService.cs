using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mottu.Fleet.Application.Services;

public class MotoService : IMotoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MotoService(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<MotoDto> CreateMotoAsync(CreateMotoDto dto)
    {
        if (await _unitOfWork.Motos.PlacaExistsAsync(dto.Placa, null))
            throw new InvalidOperationException("Placa já existe");

        var moto = _mapper.Map<Moto>(dto);
        await _unitOfWork.Motos.AddAsync(moto);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MotoDto>(moto);
    }

    public async Task<MotoDto?> GetMotoByIdAsync(int id)
    {
        var moto = await _unitOfWork.Motos.GetByIdWithPatioAsync(id);
        return moto is null ? null : _mapper.Map<MotoDto>(moto);
    }

    public async Task<MotoDto?> GetMotoByPlacaAsync(string placa)
    {
        var moto = await _unitOfWork.Motos.GetByPlacaAsync(placa);
        return moto is null ? null : _mapper.Map<MotoDto>(moto);
    }

    public async Task<IEnumerable<MotoDto>> GetMotosByPatioAsync(int patioId)
    {
        var motos = await _unitOfWork.Motos.GetByPatioAsync(patioId);
        return _mapper.Map<IEnumerable<MotoDto>>(motos);
    }

    public async Task<IEnumerable<MotoDto>> GetMotosByStatusAsync(MotoStatus status)
    {
        var motos = await _unitOfWork.Motos.GetByStatusAsync(status);
        return _mapper.Map<IEnumerable<MotoDto>>(motos);
    }

    public async Task<MotoDto?> UpdateMotoAsync(int id, UpdateMotoDto dto)
    {
        var moto = await _unitOfWork.Motos.GetByIdAsync(id);
        if (moto == null) return null;

        if (dto.Placa != null && await _unitOfWork.Motos.PlacaExistsAsync(dto.Placa, id))
            throw new InvalidOperationException("Placa já existe");

        _mapper.Map(dto, moto);
        await _unitOfWork.Motos.UpdateAsync(moto);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MotoDto>(moto);
    }

    public async Task<bool> DeleteMotoAsync(int id)
    {
        var moto = await _unitOfWork.Motos.GetByIdAsync(id);
        if (moto == null) return false;

        await _unitOfWork.Motos.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MoveMotoToPatioAsync(int motoId, int newPatioId)
    {
        var moto = await _unitOfWork.Motos.GetByIdAsync(motoId);
        if (moto == null) return false;

        moto.PatioId = newPatioId;
        await _unitOfWork.Motos.UpdateAsync(moto);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateMotoStatusAsync(int motoId, MotoStatus newStatus)
    {
        var moto = await _unitOfWork.Motos.GetByIdAsync(motoId);
        if (moto == null) return false;

        moto.Status = newStatus;
        await _unitOfWork.Motos.UpdateAsync(moto);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RegisterMaintenanceAsync(int motoId, DateTime maintenanceDate, string? observations = null)
    {
        var moto = await _unitOfWork.Motos.GetByIdAsync(motoId);
        if (moto == null) return false;

        moto.UltimaManutencao = maintenanceDate;
        moto.ProximaManutencao = maintenanceDate.AddMonths(3);
        if (observations != null)
            moto.Observacoes = observations;

        await _unitOfWork.Motos.UpdateAsync(moto);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MotoDto>> GetFleetReportAsync()
    {
        var motos = await _unitOfWork.Motos.GetAllAsync(1, int.MaxValue);
        return _mapper.Map<IEnumerable<MotoDto>>(motos);
    }

    public async Task<PagedResultDto<MotoDto>> GetMotosAsync(
        int page = 1,
        int pageSize = 10,
        string? search = null,
        int? patioId = null,
        MotoStatus? status = null)
    {
        var query = _unitOfWork.Motos.Query();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var loweredSearch = search.ToLower();
            query = query.Where(m =>
                m.Placa.ToLower().Contains(loweredSearch) ||
                m.Modelo.ToLower().Contains(loweredSearch));
        }

        if (patioId.HasValue)
        {
            query = query.Where(m => m.PatioId == patioId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(m => m.Status == status.Value);
        }

        var total = await query.CountAsync();

        var motosPaginated = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dtos = _mapper.Map<IEnumerable<MotoDto>>(motosPaginated);

        return new PagedResultDto<MotoDto>
        {
            Items = dtos,
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };
    }
}
