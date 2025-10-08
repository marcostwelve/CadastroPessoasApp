using Domain.Dto;

namespace Application.Services.Interface;

public interface IUserService
{
    Task<LoginResponseDto> Login(LoginRequestDto requestDto);
    Task<UserDto> Register(RegisterDto registerDto);
    Task<bool> IsUniqueUser(string userName);
}
