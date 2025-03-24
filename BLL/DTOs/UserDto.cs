namespace BLL.DTOs
{
    public class UserDto
    {
        public string UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequestDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string UserID { get; set; }
        public string Login { get; set; }
    }
} 