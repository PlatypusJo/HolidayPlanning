using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PatchContractorPaidDto
    {
        #region Свойства

        /// <summary>
        /// ID Подрядчика
        /// </summary>
        [Key]
        public string ContractorId { get; set; }

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public double Paid { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PatchContractorPaidDto() { }

        /// <summary>
        /// Конструктор на основе ID Подрядчика и оплаченной суммы
        /// </summary>
        /// <param name="contractorId"> ID Подрядчика </param>
        /// <param name="paid"> Оплаченная сумма </param>
        public PatchContractorPaidDto(string contractorId, double paid)
        {
            ContractorId = contractorId;
            Paid = paid;
        }

        #endregion
    }
}
