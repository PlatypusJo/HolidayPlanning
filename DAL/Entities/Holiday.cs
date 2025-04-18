﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Мероприятие
    /// </summary>
    public class Holiday
    {
        #region Свойства

        /// <summary>
        /// ID мероприятия
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
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

        /// <summary>
        /// Подрядчики мероприятия
        /// </summary>
        public virtual ICollection<Contractor> Contractors { get; set; }

        /// <summary>
        /// Гости мероприятия
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        /// <summary>
        /// Задачи мероприятия
        /// </summary>
        public virtual ICollection<Goal> Goals { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Holiday() 
        {
            Contractors = new HashSet<Contractor>();
            Members = new HashSet<Member>();
            Goals = new HashSet<Goal>();
        }
        
        #endregion
    }
}
