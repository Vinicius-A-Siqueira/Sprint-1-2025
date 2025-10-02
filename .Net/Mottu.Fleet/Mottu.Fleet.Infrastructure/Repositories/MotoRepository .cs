using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Infrastructure.Repositories;

public class MotoRepository : IMotoRepository
{
    private readonly FleetDbContext _context;

    public MotoRepository(FleetDbContext context)
    {
        _context = context;
    }

    public IQueryable<Moto> Query()
    {
        return _context.Motos.AsQueryable();
    }

    public async Task<IEnumerable<Moto>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Motos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Moto?> GetByIdAsync(int id)
    {
        return await _context.Motos.FindAsync(id);
    }

    public async Task<Moto?> GetByIdWithPatioAsync(int id)
    {
        return await _context.Motos
            .Include(m => m.Patio)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Moto?> GetByPlacaAsync(string placa)
    {
        return await _context.Motos
            .FirstOrDefaultAsync(m => m.Placa == placa);
    }

    public async Task<IEnumerable<Moto>> GetByPatioAsync(int patioId)
    {
        return await _context.Motos
            .Where(m => m.PatioId == patioId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Moto>> GetByStatusAsync(MotoStatus status)
    {
        return await _context.Motos
            .Where(m => m.Status == status)
            .ToListAsync();
    }

    public async Task AddAsync(Moto moto)
    {
        await _context.Motos.AddAsync(moto);
    }

    public async Task UpdateAsync(Moto moto)
    {
        _context.Motos.Update(moto);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var moto = await GetByIdAsync(id);
        if (moto != null)
            _context.Motos.Remove(moto);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Motos.CountAsync();
    }

    public async Task<bool> PlacaExistsAsync(string placa, int? excludeId)
    {
        var query = _context.Motos.Where(m => m.Placa == placa);

        if (excludeId.HasValue)
            query = query.Where(m => m.Id != excludeId.Value);

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Moto>> SearchAsync(string searchTerm)
    {
        var loweredSearch = searchTerm.ToLower();
        return await _context.Motos
            .Where(m => m.Placa.ToLower().Contains(loweredSearch) || m.Modelo.ToLower().Contains(loweredSearch))
            .ToListAsync();
    }
}
