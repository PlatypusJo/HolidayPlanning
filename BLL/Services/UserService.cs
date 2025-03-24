using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto loginRequest)
        {
            try
            {
                Console.WriteLine($"Attempting login for user: {loginRequest.Login}");
                
                // Получаем пользователя напрямую из Firestore
                var user = await _unitOfWork.User.GetUserByLogin(loginRequest.Login);
                
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return null;
                }
                
                Console.WriteLine($"Found user: {user.Login}, checking password");
                
                // Простая проверка пароля (без хеширования)
                if (user.Password != loginRequest.Password)
                {
                    Console.WriteLine("Password incorrect");
                    return null;
                }
                
                Console.WriteLine("Login successful");
                
                // Используем ID документа как UserID, если UserID не задан
                string userIdToReturn = !string.IsNullOrEmpty(user.UserID) ? user.UserID : user.Id;
                
                return new AuthResponseDto
                {
                    UserID = userIdToReturn,
                    Login = user.Login
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return null;
            }
        }

        public async Task<AuthResponseDto> Register(RegisterRequestDto registerRequest)
        {
            try
            {
                Console.WriteLine($"Attempting to register user: {registerRequest.Login}");
                
                // Проверка существования пользователя
                if (await UserExists(registerRequest.Login))
                {
                    Console.WriteLine("User already exists");
                    return null;
                }
                
                // Создание нового пользователя
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(), // Устанавливаем Id для Identity
                    UserID = Guid.NewGuid().ToString(),
                    Login = registerRequest.Login,
                    Password = registerRequest.Password,
                    UserName = registerRequest.Login // Для Identity
                };
                
                Console.WriteLine($"Created user object: ID={user.UserID}, Login={user.Login}");
                
                try
                {
                    // Сохраняем пользователя напрямую в Firestore
                    var docRef = await _unitOfWork.User.CreateFirestoreUser(user);
                    
                    if (docRef == null)
                    {
                        Console.WriteLine("Failed to create user in Firestore");
                        return null;
                    }
                    
                    Console.WriteLine($"User registered successfully with document ID: {docRef}");
                    
                    return new AuthResponseDto
                    {
                        UserID = user.UserID,
                        Login = user.Login
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating user: {ex.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            try
            {
                Console.WriteLine($"Searching for user with ID: {userId}");
                
                var user = await _unitOfWork.User.GetUserById(userId);
                
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return null;
                }
                
                Console.WriteLine($"Found user: ID={user.UserID}, Login={user.Login}");
                
                // Используем ID документа как UserID, если UserID не задан
                string userIdToReturn = !string.IsNullOrEmpty(user.UserID) ? user.UserID : user.Id;
                
                return new UserDto
                {
                    UserID = userIdToReturn,
                    Login = user.Login,
                    Password = null // Не возвращаем пароль
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UserExists(string login)
        {
            try
            {
                return await _unitOfWork.User.ExistsByLogin(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking if user exists: {ex.Message}");
                return false;
            }
        }
    }
} 