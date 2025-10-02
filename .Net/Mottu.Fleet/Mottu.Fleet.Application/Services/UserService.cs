using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Application.Interfaces;

namespace Mottu.Fleet.Application.Services;
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        if (await _unitOfWork.Users.UsernameExistsAsync(dto.Username, null)) // <-- Passa null para excludeId
            throw new InvalidOperationException("Username já existe");

        var user = _mapper.Map<User>(dto);
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(username);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return null;

        if (dto.Username != null &&
            await _unitOfWork.Users.UsernameExistsAsync(dto.Username, id))
            throw new InvalidOperationException("Username já existe");

        _mapper.Map(dto, user);
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null) return false;

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto?> AuthenticateAsync(string username, string password)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(username);
        if (user == null || user.Password != password)
            return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateLastLoginAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user != null)
        {
            user.LastLogin = DateTime.UtcNow;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<PagedResultDto<UserDto>> GetUsersAsync(int page = 1, int pageSize = 10, string? search = null)
    {
        var users = await _unitOfWork.Users.GetAllAsync(page, pageSize);
        int total = await _unitOfWork.Users.CountAsync();

        var dtos = _mapper.Map<IEnumerable<UserDto>>(users);
        var result = new PagedResultDto<UserDto>
        {
            Items = dtos,
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };

        return result;
    }
}
