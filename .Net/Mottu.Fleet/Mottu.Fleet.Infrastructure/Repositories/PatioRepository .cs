using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mottu.Fleet.Infrastructure.Repositories;

public class PatioRepository : IPatioRepository
{
    private readonly FleetDbContext _context;

    public PatioRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patio>> GetAllAsync(int page, int pageSize) =>
        await _context.Patios.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    public async Task<Patio?> GetByIdAsync(int id) =>
        await _context.Patios.FindAsync(id);

    public async Task<Patio?> GetByIdWithMotosAsync(int id) =>
        await _context.Patios.Include(p => p.Motos).FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Patio patio) => await _context.Patios.AddAsync(patio);

    public async Task UpdateAsync(Patio patio)
    {
        _context.Patios.Update(patio);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var patio = await _context.Patios.FindAsync(id);
        if (patio != null) _context.Patios.Remove(patio);
    }

    public async Task<int> CountAsync() => await _context.Patios.CountAsync();

    public async Task<IEnumerable<Patio>> SearchAsync(string searchTerm) =>
        await _context.Patios.Where(p => p.Nome.Contains(searchTerm)).ToListAsync();
}
