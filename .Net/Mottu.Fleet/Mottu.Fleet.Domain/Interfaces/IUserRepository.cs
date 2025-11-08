using System.Collections.Generic;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UsuarioMongo>> GetAllAsync(int page, int pageSize);
    Task<UsuarioMongo?> GetByIdAsync(int id);
    Task<UsuarioMongo?> GetByUsernameAsync(string username);
    Task AddAsync(UsuarioMongo user);
    Task UpdateAsync(UsuarioMongo user);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<bool> UsernameExistsAsync(string username, int? excludeId = null);
}

