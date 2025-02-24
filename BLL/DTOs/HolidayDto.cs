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
        /// Целочисленный ID мероприятия
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название мероприятия
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Дата и время начала мероприятия
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Дата и время конца мероприятия
        /// </summary>
        public DateTime EndTime { get; set; }
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
        public HolidayDto(Holiday holiday)
        {
            Id = holiday.Id;
            Title = holiday.Title;
            StartTime = holiday.StartTime;
            EndTime = holiday.EndTime;
            Budget = holiday.Budget;
        }
        #endregion
    }
}
