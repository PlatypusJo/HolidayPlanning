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
    /// Dto-класс для изменения статуса подрядчика в сущности подрядчика методом Patch
    /// </summary>
    public class PatchContractorStatusDto
    {
        #region Свойства

        /// <summary>
        /// ID Подрядчика
        /// </summary>
        [Key]
        public string ContractorId { get; set; }

        /// <summary>
        /// ID Статуса подрядчика
        /// </summary>
        public string ContractorStatusId { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PatchContractorStatusDto() { }

        /// <summary>
        /// Конструктор на основе ID Подрядчика и ID Статуса подрядчика
        /// </summary>
        /// <param name="contractorStatusId">ID Статуса подрядчика</param>
        /// <param name="contractorId">ID Подрядчика</param>
        public PatchContractorStatusDto(string contractorId, string contractorStatusId)
        {
            ContractorId = contractorId;
            ContractorStatusId = contractorStatusId;
        }

        #endregion
    }
}
