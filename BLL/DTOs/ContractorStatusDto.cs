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
    /// DTO-класс Статус подрядчика
    /// </summary>
    public class ContractorStatusDto
    {
        #region Свойства

        /// <summary>
        /// ID Статуса
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string? Title { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ContractorStatusDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="contractorStatus">Сущность Статус подрядчика</param>
        public ContractorStatusDto(ContractorStatus contractorStatus)
        {
            Id = contractorStatus.Id;
            Title = contractorStatus.Title;
        }

        #endregion
    }
}
