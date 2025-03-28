using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Категория меню
    /// </summary>
    public class MenuCategory
    {
        #region Свойства

        /// <summary>
        /// ID Категории меню
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Гости с меню такой категории
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MenuCategory()
        {
            Members = new HashSet<Member>();
        }

        #endregion
    }
}
