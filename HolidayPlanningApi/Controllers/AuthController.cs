using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HolidayPlanningApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Login(loginRequest);
            
            if (result == null)
            {
                return Unauthorized("Неверный логин или пароль");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.UserExists(registerRequest.Login))
            {
                return BadRequest("Пользователь с таким логином уже существует");
            }

            var result = await _userService.Register(registerRequest);
            
            if (result == null)
            {
                return BadRequest("Не удалось зарегистрировать пользователя");
            }

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _userService.GetUserById(userId);
            
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            // Не возвращаем пароль в ответе
            user.Password = null;
            
            return Ok(user);
        }
    }
} 