using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByUsernameAsync(string username);
    Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteUserAsync(int id);
    Task<UserDto?> AuthenticateAsync(string username, string password);
    Task UpdateLastLoginAsync(int userId);
    Task<PagedResultDto<UserDto>> GetUsersAsync(int page = 1, int pageSize = 10, string? search = null);
}
