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
    /// DTO-класс Подрядчика
    /// </summary>
    public class ContractorDto
    {
        #region Свойства

        /// <summary>
        /// ID Подрядчика
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID Мероприятия, на которое нанят подрядчик
        /// </summary>
        public string HolidayId { get; set; }

        /// <summary>
        /// ID статуса найма подрядчика (Ожидание, Принял, Отказал)
        /// </summary>
        public string ContractorStatusId { get; set; }

        /// <summary>
        /// ID категории деятельности подрядчика
        /// </summary>
        public string ContractorСategoryId { get; set; }

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
        /// Оплаченная сумма от всей цены
        /// </summary>
        public double Paid { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ContractorDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="contractor">Сущность Подрядчик</param>
        public ContractorDto(Contractor contractor)
        {
            Id = contractor.Id;
            HolidayId = contractor.HolidayId;
            ContractorStatusId = contractor.ContractorStatusId;
            ContractorСategoryId = contractor.ContractorСategoryId;
            Title = contractor.Title;
            Description = contractor.Description;
            PhoneNumber = contractor.PhoneNumber;
            Email = contractor.Email;
            ServiceCost = contractor.ServiceCost;
            Paid = contractor.Paid;
        }

        #endregion
    }
}
