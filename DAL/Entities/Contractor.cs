using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Подрядчик мероприятия
    /// </summary>
    public class Contractor
    {
        #region Свойства

        /// <summary>
        /// ID Подрядчика
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// ID пользователя, который нанял подрядчика
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ID Мероприятия, на которое нанят подрядчик
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// ID статуса найма подрядчика (Ожидание, Принял, Отказал)
        /// </summary>
        public string StatusId { get; set; }

        /// <summary>
        /// ID категории деятельности подрядчика
        /// </summary>
        public string СategoryId { get; set; }

        /// <summary>
        /// ФИО Подрядчика или название организации
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание работы подрядчика
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Номер телефона подрядчика
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты подрядчика
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Цена услуги подрядчика (цена найма)
        /// </summary>
        public double ServiceCost { get; set; }

        /// <summary>
        /// Мероприятие, на которое нанят подрядчик
        /// </summary>
        public virtual Holiday Holiday { get; set; }

        /// <summary>
        /// Категории деятельности подрядчика
        /// </summary>
        public virtual ContractorCategory ContractorCategory { get; set; }

        /// <summary>
        /// Статус найма подрядчика (Ожидание, Принял, Отказал)
        /// </summary>
        public virtual ContractorStatus ContractorStatus { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Contractor() { }

        #endregion
    }
}
