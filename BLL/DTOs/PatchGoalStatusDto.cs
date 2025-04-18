using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    /// <summary>
    /// Dto-класс для изменения статуса задачи в сущности задачи методом Patch
    /// </summary>
    public class PatchGoalStatusDto
    {
        #region Свойства

        /// <summary>
        /// ID Задачи
        /// </summary>
        [Key]
        public string GoalId { get; set; }

        /// <summary>
        /// ID Статуса задачи
        /// </summary>
        public string GoalStatusId { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PatchGoalStatusDto() { }

        /// <summary>
        /// Конструктор на основе ID Задачи и ID Статуса задачи
        /// </summary>
        /// <param name="goalStatusId">ID Статуса задачи</param>
        /// <param name="goalId">ID Задачи</param>
        public PatchGoalStatusDto(string goalId, string goalStatusId)
        {
            GoalId = goalId;
            GoalStatusId = goalStatusId;
        }

        #endregion
    }
}
