using Application.Services.Interface;
using Domain.Dto;
using Infrastructure.Repositories.Interface;

namespace Application.Services;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }   
    public Task<bool> IsUniqueUser(string userName)
    {
        var isUniqueUser = _userRepository.IsUniqueUser(userName);

        return isUniqueUser;
    }

    public Task<LoginResponseDto> Login(LoginRequestDto requestDto)
    {
        if (requestDto == null)
        {
            throw new ArgumentNullException(nameof(requestDto));    
        }

        var userLogin = _userRepository.Login(requestDto);
        return userLogin;
    }

    public Task<UserDto> Register(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            throw new ArgumentNullException(nameof(registerDto));
        }

        var userRegister = _userRepository.Register(registerDto);
        return userRegister;
    }
}
