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
    /// DTO-класс Гостя
    /// </summary>
    public class MemberDto
    {
        #region Свойства

        /// <summary>
        /// ID Гостя
        /// </summary>
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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MemberDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="member">Сущность Гость</param>
        public MemberDto(Member member)
        {
            Id = member.Id;
            HolidayId = member.HolidayId;
            MemberCategoryId = member.MemberCategoryId;
            MemberStatusId = member.MemberStatusId;
            MenuCategoryId = member.MenuCategoryId;
            FIO = member.FIO;
            PhoneNumber = member.PhoneNumber;
            Email = member.Email;
            Comment = member.Comment;
            IsChild = member.IsChild;
            IsMale = member.IsMale;
            Seat = member.Seat;
        }

        #endregion
    }
}
