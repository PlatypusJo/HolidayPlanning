using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Мероприятие
    /// </summary>
    public class Holiday
    {
        #region Свойства

        /// <summary>
        /// ID мероприятия
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название мероприятия
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Дата и время начала мероприятия
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата и время конца мероприятия
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Бюджет мероприятия в рублях
        /// </summary>
        public double Budget { get; set; }

        /// <summary>
        /// Подрядчики мероприятия
        /// </summary>
        public virtual ICollection<Contractor> Contractors { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Holiday() 
        {
            Contractors = new HashSet<Contractor>();
        }
        
        #endregion
    }
}
