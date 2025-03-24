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

        public async Task<User> GetUserByLogin(string login)
        {
            try
            {
                var query = _db.Collection("users").WhereEqualTo("login", login);
                var snapshot = await query.GetSnapshotAsync();
                
                if (snapshot.Count == 0)
                    return null;
                    
                var userDoc = snapshot.Documents[0];
                var user = userDoc.ConvertTo<User>();
                
                // Если UserID не заполнен, используем ID документа
                if (string.IsNullOrEmpty(user.UserID))
                {
                    user.UserID = userDoc.Id;
                }
                
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserByLogin: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ExistsByLogin(string login)
        {
            var query = _db.Collection("users").WhereEqualTo("login", login);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Count > 0;
        }

        public async Task<User> GetUserById(string userId)
        {
            var query = _db.Collection("users").WhereEqualTo("UserID", userId);
            var snapshot = await query.GetSnapshotAsync();
            
            if (snapshot.Count == 0)
            {
                // Попробуем поискать по ID документа
                try
                {
                    var docRef = _db.Collection("users").Document(userId);
                    var docSnapshot = await docRef.GetSnapshotAsync();
                    
                    if (docSnapshot.Exists)
                    {
                        var user = docSnapshot.ConvertTo<User>();
                        // Если UserID не заполнен, используем ID документа
                        if (string.IsNullOrEmpty(user.UserID))
                        {
                            user.UserID = docSnapshot.Id;
                        }
                        return user;
                    }
                    
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            
            var userDoc = snapshot.Documents[0];
            var result = userDoc.ConvertTo<User>();
            
            // Если UserID не заполнен, используем ID документа
            if (string.IsNullOrEmpty(result.UserID))
            {
                result.UserID = userDoc.Id;
            }
            
            return result;
        }

        public async Task<string> CreateFirestoreUser(User user)
        {
            try
            {
                // Создаем новый документ в коллекции users
                var docRef = _db.Collection("users").Document();
                
                // Создаем словарь с данными пользователя
                var userData = new Dictionary<string, object>
                {
                    { "UserID", user.UserID },
                    { "login", user.Login },
                    { "password", user.Password }
                };
                
                // Сохраняем данные в Firestore
                await docRef.SetAsync(userData);
                
                // Возвращаем ID документа
                return docRef.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateFirestoreUser: {ex.Message}");
                return null;
            }
        }
    }
}
