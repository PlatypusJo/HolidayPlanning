using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class HolidayExpenseDto
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

        /// <summary>
        /// Флаг является ли статья расходов услугой подрядчика
        /// </summary>
        public bool IsContractor { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public HolidayExpenseDto() { }

        /// <summary>
        /// Конструктор на основе сущности Статьи расходов
        /// </summary>
        /// <param name="expense">Сущность статьи расходов</param>
        public HolidayExpenseDto(Expense expense)
        {
            Id = expense.Id;
            HolidayId = expense.HolidayId;
            Title = expense.Title;
            Description = expense.Description;
            Amount = expense.Amount;
            Paid = expense.Paid;
            IsContractor = false;
        }

        /// <summary>
        /// Конструктор на основе сущности Подрядчика
        /// </summary>
        /// <param name="contactor">Сущность Подрядчика</param>
        public HolidayExpenseDto(Contractor contactor)
        {
            Id = contactor.Id;
            HolidayId = contactor.HolidayId;
            Title = contactor.Title;
            Description = contactor.Description;
            Amount = contactor.ServiceCost;
            Paid = contactor.Paid;
            IsContractor = true;
        }

        #endregion
    }
}
