using System.Linq;
using Mottu.Fleet.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IMotoRepository
{
    IQueryable<MotoMongo> Query();
    Task<IEnumerable<MotoMongo>> GetAllAsync(int page, int pageSize);
    Task<MotoMongo?> GetByIdAsync(int id);
    Task<MotoMongo?> GetByPlacaAsync(string placa);
    Task<MotoMongo?> GetByIdWithPatioAsync(int id);
    Task<IEnumerable<MotoMongo>> GetByPatioAsync(int patioId);
    Task AddAsync(MotoMongo moto);
    Task UpdateAsync(MotoMongo moto);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<bool> PlacaExistsAsync(string placa, int? excludeId);
    Task<IEnumerable<MotoMongo>> SearchAsync(string searchTerm);
}
