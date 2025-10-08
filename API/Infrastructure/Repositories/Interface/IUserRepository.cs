using Domain.Dto;

namespace Infrastructure.Repositories.Interface;

public interface IUserRepository
{
    Task<bool> IsUniqueUser(string userName);
    Task<LoginResponseDto> Login(LoginRequestDto registrationRequestDto);

    Task<UserDto> Register(RegisterDto registerDto);
}
