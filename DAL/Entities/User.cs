using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    [FirestoreData]
    public class User : IdentityUser<string>
    {
        #region Свойства

        /// <summary>
        /// id пользователя.
        /// </summary>
        [FirestoreProperty("UserID")]
        public string UserID { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [FirestoreProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [FirestoreProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Мероприятия пользователя.
        /// </summary>
        public virtual ICollection<Holiday> Holidays { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public User()
        {
            Holidays = new HashSet<Holiday>();
        }

        #endregion
    }
}
