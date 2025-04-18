using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    /// <summary>
    /// Dto-класс Задача мероприятия
    /// </summary>
    public class GoalDto
    {
        #region Свойства

        /// <summary>
        /// ID Задачи
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID Мероприятия, к которому относится задача
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// ID статуса выполнения задачи (в ожидании, подтверждена, отклонена)
        /// </summary>
        public string GoalStatusId { get; set; }

        /// <summary>
        /// Название/описание задачи
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Дата и время, до которого задачу нужно выполнить
        /// </summary>
        public DateTime Deadline { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public GoalDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="goal">Сущность задача</param>
        public GoalDto(Goal goal) 
        {
            Id = goal.Id;
            HolidayId = goal.HolidayId;
            GoalStatusId = goal.GoalStatusId;
            Title = goal.Title;
            Deadline = goal.Deadline;
        }

        #endregion
    }
}
