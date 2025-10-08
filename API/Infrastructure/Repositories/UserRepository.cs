using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;

    public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
        _secretKey = _configuration["ApiSettings:SecretKey"] ?? throw new ArgumentNullException("ApiSettings:SecretKey não está configurada");
    }
    public async Task<LoginResponseDto> Login(LoginRequestDto registrationRequestDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName.ToLower() == registrationRequestDto.UserName.ToLower());


        if (user == null)
        {
            return new LoginResponseDto()
            {
                Token = "",
                User = null,
            };
        }

        bool isValid = await _userManager.CheckPasswordAsync(user, registrationRequestDto.Password);

        if (!isValid)
        {
            return new LoginResponseDto()
            {
                Token = "",
                User = null,
            };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            Token = tokenHandler.WriteToken(token),
            User = _mapper.Map<UserDto>(user)
        };

        return loginResponseDto;
    }

    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        ApplicationUser user = new()
        {
            UserName = registerDto.UserName,
            Email = registerDto.UserName,
            NormalizedEmail = registerDto.UserName.ToUpper(),
            Name = registerDto.Name,
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var userToken = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == registerDto.UserName.ToLower());
                return _mapper.Map<UserDto>(userToken);
            }

            else
            {
                return new UserDto();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar usuário: " + ex.Message);
        }
    }

    public async Task<bool> IsUniqueUser(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());

        if (user == null)
        {
            return true;
        }

        return false;
    }
}
