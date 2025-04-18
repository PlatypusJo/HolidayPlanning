using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Задача мероприятия
    /// </summary>
    public class Goal
    {
        #region Свойства

        /// <summary>
        /// ID Задачи
        /// </summary>
        [Key]
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

        /// <summary>
        /// Мероприятие, к которому относится задача
        /// </summary>
        public virtual Holiday Holiday { get; set; }

        /// <summary>
        /// Статус выполнения задачи (в ожидании, подтверждена, отклонена)
        /// </summary>
        public virtual GoalStatus GoalStatus { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Goal() { }

        #endregion
    }
}
