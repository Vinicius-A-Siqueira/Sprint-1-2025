using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mottu.Fleet.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FleetDbContext _context;

    public UserRepository(FleetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync(int page, int pageSize) =>
        await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    public async Task<User?> GetByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null) _context.Users.Remove(user);
    }

    public async Task<int> CountAsync() =>
        await _context.Users.CountAsync();

    public async Task<bool> UsernameExistsAsync(string username, int? excludeId = null)
    {
        var query = _context.Users.Where(u => u.Username == username);
        if (excludeId.HasValue)
            query = query.Where(u => u.Id != excludeId.Value);
        return await query.AnyAsync();
    }
}
