using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class MotoService : IMotoService
{
    private readonly MottuDbContext _context;
    private readonly IMapper _mapper;

    public MotoService(MottuDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MotoDTO>> ObterTodos()
    {
        var motos = await _context.Motos.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<MotoDTO>>(motos);
    }

    public async Task<MotoDTO?> ObterPorId(int id)
    {
        var moto = await _context.Motos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (moto == null) return null;
        return _mapper.Map<MotoDTO>(moto);
    }

    public async Task Criar(CreateMotoDTO dto)
    {
        var moto = new Moto(dto.Placa, dto.PatioId, dto.PatioFilialIdFilial, dto.AnoFabricacao);
        _context.Motos.Add(moto);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(int id, CreateMotoDTO dto)
    {
        var moto = await _context.Motos.FindAsync(id);
        if (moto == null)
            throw new Exception("Moto não encontrada.");

        moto.AtualizarPlaca(dto.Placa);
        moto.AtualizarAnoFabricacao(dto.AnoFabricacao);
        moto.AtualizarPatio(dto.PatioId, dto.PatioFilialIdFilial);

        await _context.SaveChangesAsync();
    }

    public async Task Deletar(int id)
    {
        var moto = await _context.Motos.FindAsync(id);
        if (moto == null)
            throw new Exception("Moto não encontrada.");

        _context.Motos.Remove(moto);
        await _context.SaveChangesAsync();
    }
}
