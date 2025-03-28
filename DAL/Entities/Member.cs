using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Гость мероприятия
    /// </summary>
    public class Member
    {

        #region Свойства

        /// <summary>
        /// ID Гостя
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// ID Мероприятия, на которое приглашен гость
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// ID Категории гостя
        /// </summary>
        public string MemberCategoryId { get; set; }

        /// <summary>
        /// ID Статуса гостя
        /// </summary>
        public string MemberStatusId { get; set; }

        /// <summary>
        /// ID Категории меню гостя
        /// </summary>
        public string MenuCategoryId { get; set; }

        /// <summary>
        /// ФИО Гостя
        /// </summary>
        public string? FIO { get; set; }

        /// <summary>
        /// Номер телефона гостя
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты гостя
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Комментарий к гостю
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Возраст (true - ребенок, false - взрослый)
        /// </summary>
        public bool IsChild { get; set; }

        /// <summary>
        /// Пол (true - мужчина, false - женщина)
        /// </summary>
        public bool IsMale { get; set; }

        /// <summary>
        /// Информация о месте для гостя
        /// </summary>
        public string? Seat { get; set; }

        /// <summary>
        /// Мероприятие, на которое приглашен гость
        /// </summary>
        public virtual Holiday Holiday { get; set; }
        
        /// <summary>
        /// Категория меню гостя
        /// </summary>
        public virtual MenuCategory MenuCategory { get; set; }
        
        /// <summary>
        /// Категория гостя
        /// </summary>
        public virtual MemberCategory MemberCategory { get; set; }

        /// <summary>
        /// Статус гостя
        /// </summary>
        public virtual MemberStatus MemberStatus { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Member() { }

        #endregion
    }
}
