using System.Collections.Generic;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(int page, int pageSize);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
    Task<bool> UsernameExistsAsync(string username, int? excludeId = null);
}

