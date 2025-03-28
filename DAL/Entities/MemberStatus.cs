using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Статус гостя
    /// </summary>
    public class MemberStatus
    {
        #region Свойства

        /// <summary>
        /// ID Статуса
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Гости статуса
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MemberStatus()
        {
            Members = new HashSet<Member>();
        }

        #endregion
    }
}
