using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    /// <summary>
    /// DTO-класс Мероприятие
    /// </summary>
    public class HolidayDto
    {
        #region Свойства

        /// <summary>
        /// ID мероприятия
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        public string UserId { get; set; }

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
        
        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public HolidayDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="holiday">Сущность Мероприятие</param>
        public HolidayDto(Holiday holiday)
        {
            Id = holiday.Id;
            UserId = holiday.UserId;
            Title = holiday.Title;
            StartDate = holiday.StartDate;
            EndDate = holiday.EndDate;
            Budget = holiday.Budget;
        }
        
        #endregion
    }
}
