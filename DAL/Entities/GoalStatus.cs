using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Статус задачи
    /// </summary>
    public class GoalStatus
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

        /// <summary>
        /// Задачи статуса
        /// </summary>
        public virtual ICollection<Goal> Goals { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public GoalStatus()
        {
            Goals = new HashSet<Goal>();
        }

        #endregion
    }
}
