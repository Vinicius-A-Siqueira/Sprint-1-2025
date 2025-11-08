using System.Collections.Generic;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IPatioRepository
{
    Task<IEnumerable<PatioMongo>> GetAllAsync(int page, int pageSize);
    Task<PatioMongo?> GetByIdAsync(int id);
    Task<PatioMongo?> GetByIdWithMotosAsync(int id);
    Task AddAsync(PatioMongo patio);
    Task UpdateAsync(PatioMongo patio);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<IEnumerable<PatioMongo>> SearchAsync(string searchTerm);
}

