﻿using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository<T> where T: IdentityUser<string>
    {
        Task<List<T>> GetAll(UserManager<User> userManager);

        Task<T> GetItem(string login, UserManager<T> userManager);
        Task<IdentityResult> Create(T item, string password, UserManager<T> userManager);
        Task<IdentityResult> Delete(string login, UserManager<T> userManager);
        Task<bool> Exists(string login, UserManager<T> userManager);
        Task<User> GetUserByLogin(string login);
        Task<bool> ExistsByLogin(string login);
        Task<User> GetUserById(string userId);
        Task<string> CreateFirestoreUser(User user);
    }
}