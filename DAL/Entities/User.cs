using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : IdentityUser<string>
    {
        #region Свойства

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string? Login { get; set; }

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
