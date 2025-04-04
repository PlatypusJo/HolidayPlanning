using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность статьи расходов.
    /// </summary>
    public class Expense
    {
        #region Свойства

        /// <summary>
        /// ID статьи расходов.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// ID мероприятия, к которому относится статья расходов.
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// Название статьи расходов.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание статьи расходов.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Объём расходов.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Сколько уже оплачено.
        /// </summary>
        public double Paid { get; set; }

        /// <summary>
        /// Мероприятие, к которому относится статья расходов.
        /// </summary>
        public virtual Holiday Holiday { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Expense() { }

        #endregion
    }
}
