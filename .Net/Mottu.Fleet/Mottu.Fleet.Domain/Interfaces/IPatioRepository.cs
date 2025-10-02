using System.Collections.Generic;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IPatioRepository
{
    Task<IEnumerable<Patio>> GetAllAsync(int page, int pageSize);
    Task<Patio?> GetByIdAsync(int id);
    Task<Patio?> GetByIdWithMotosAsync(int id);
    Task AddAsync(Patio patio);
    Task UpdateAsync(Patio patio);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<IEnumerable<Patio>> SearchAsync(string searchTerm);
}

