using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ExpenseDto
    {
        #region Свойства

        /// <summary>
        /// ID статьи расходов
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID мероприятия, к которому относится статья расходов
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// Название статьи расходов
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание статьи расходов
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Объём расходов
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Сколько уже оплачено
        /// </summary>
        public double Paid { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ExpenseDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="expense">Сущность статьи расходов</param>
        public ExpenseDto(Expense expense)
        {
            Id = expense.Id;
            HolidayId = expense.HolidayId;
            Title = expense.Title;
            Description = expense.Description;
            Amount = expense.Amount;
            Paid = expense.Paid;
        }

        #endregion
    }
}
