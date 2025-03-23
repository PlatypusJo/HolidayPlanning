using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly FirestoreDb _db;

        public UserRepository(FirestoreDb db) => _db = db;

        public async Task<IdentityResult> Create(User item, string password, UserManager<User> userManager)
            => await userManager.CreateAsync(item, password);

        public async Task<IdentityResult> Delete(string login, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(login);
            IdentityResult result = new IdentityResult();
            if (user != null)
            {
                result = await userManager.DeleteAsync(user);
            }
            return result;
        }

        public async Task<bool> Exists(string login, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(login);


            return user != null;
        }

        public async Task<List<User>> GetAll(UserManager<User> userManager)
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<User> GetItem(string login, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(login);
            return user;
        }
    }
}
