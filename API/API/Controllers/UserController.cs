using Application.Services.Interface;
using Azure;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        try
        {
            var loginResponse = await _userService.Login(model);
            if (loginResponse == null) 
            {
                return Unauthorized("Usuário ou senha inválidos!");
            }
            var response = new LoginResponseDto
            {
                User = loginResponse.User,
                Token = loginResponse.Token
            };
            return Ok(response);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno do servidor: " + ex.Message);
        }

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        try
        {
            bool ifUserNameUnique = await _userService.IsUniqueUser(model.UserName);

            var user = await _userService.Register(model);
            if (user == null)
            {

                return BadRequest("Erro ao registrar usuário!");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno do servidor: " + ex.Message);
        }
       

    }
}
