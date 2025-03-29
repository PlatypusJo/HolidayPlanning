using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Категория гостя
    /// </summary>
    public class MemberCategory
    {
        #region Свойства

        /// <summary>
        /// ID Категории гостя
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Гости категории
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MemberCategory()
        {
            Members = new HashSet<Member>();
        }

        #endregion
    }
}
