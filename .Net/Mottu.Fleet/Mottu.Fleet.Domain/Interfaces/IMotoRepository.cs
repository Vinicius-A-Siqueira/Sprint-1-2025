using System.Linq;
using Mottu.Fleet.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IMotoRepository
{
    IQueryable<Moto> Query();
    Task<IEnumerable<Moto>> GetAllAsync(int page, int pageSize);
    Task<Moto?> GetByIdAsync(int id);
    Task<Moto?> GetByPlacaAsync(string placa);
    Task<Moto?> GetByIdWithPatioAsync(int id);
    Task<IEnumerable<Moto>> GetByPatioAsync(int patioId);
    Task<IEnumerable<Moto>> GetByStatusAsync(MotoStatus status);
    Task AddAsync(Moto moto);
    Task UpdateAsync(Moto moto);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<bool> PlacaExistsAsync(string placa, int? excludeId);
    Task<IEnumerable<Moto>> SearchAsync(string searchTerm);
}
