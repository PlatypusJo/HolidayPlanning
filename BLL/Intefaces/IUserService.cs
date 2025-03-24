using BLL.DTOs;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    public interface IUserService
    {
        Task<AuthResponseDto> Login(LoginRequestDto loginRequest);
        Task<AuthResponseDto> Register(RegisterRequestDto registerRequest);
        Task<UserDto> GetUserById(string userId);
        Task<bool> UserExists(string login);
    }
} 