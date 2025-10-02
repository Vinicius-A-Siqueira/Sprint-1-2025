using Microsoft.AspNetCore.Mvc;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Services;
using Mottu.Fleet.Application.Interfaces;

namespace Mottu.Fleet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<UserDto>>> GetUsers(int page = 1, int pageSize = 10, string? search = null)
    {
        var users = await _userService.GetUsersAsync(page, pageSize, search);
        AddPaginationLinks(users, page, pageSize, search);
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound($"Usuário com ID {id} não encontrado.");
        AddUserLinks(user);
        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null) return NotFound($"Usuário '{username}' não encontrado.");
        AddUserLinks(user);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var user = await _userService.CreateUserAsync(dto);
            AddUserLinks(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var user = await _userService.UpdateUserAsync(id, dto);
            if (user == null) return NotFound($"Usuário com ID {id} não encontrado.");
            AddUserLinks(user);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success) return NotFound($"Usuário com ID {id} não encontrado.");
        return NoContent();
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<UserDto>> Authenticate([FromBody] LoginRequest login)
    {
        var user = await _userService.AuthenticateAsync(login.Username, login.Password);
        if (user == null) return Unauthorized("Credenciais inválidas.");
        await _userService.UpdateLastLoginAsync(user.Id);
        AddUserLinks(user);
        return Ok(user);
    }

    private void AddUserLinks(UserDto user)
    {
        user.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetUser), new { id = user.Id })! },
            { "update", Url.Action(nameof(UpdateUser), new { id = user.Id })! },
            { "delete", Url.Action(nameof(DeleteUser), new { id = user.Id })! },
            { "all", Url.Action(nameof(GetUsers))! }
        };
    }

    private void AddPaginationLinks(PagedResultDto<UserDto> result, int page, int pageSize, string? search)
    {
        result.Links = new Dictionary<string, string>
        {
            { "self", Url.Action(nameof(GetUsers), new { page, pageSize, search })! }
        };

        if (result.HasPrevious)
        {
            result.Links.Add("previous", Url.Action(nameof(GetUsers), new { page = page - 1, pageSize, search })!);
        }
        if (result.HasNext)
        {
            result.Links.Add("next", Url.Action(nameof(GetUsers), new { page = page + 1, pageSize, search })!);
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
