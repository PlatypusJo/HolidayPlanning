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
    /// DTO-класс Статуса задачи
    /// </summary>
    public class GoalStatusDto
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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public GoalStatusDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="goalStatus">Сущность Статус задачи</param>
        public GoalStatusDto(GoalStatus goalStatus)
        {
            Id = goalStatus.Id;
            Title = goalStatus.Title;
        }

        #endregion
    }
}
