using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PatchExpensePaidDto
    {
        #region Свойства

        /// <summary>
        /// ID Статьи расходов
        /// </summary>
        [Key]
        public string ExpenseId { get; set; }

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public double Paid { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PatchExpensePaidDto() { }

        /// <summary>
        /// Конструктор на основе ID Статьи расходов и оплаченной суммы
        /// </summary>
        /// <param name="expenseId"> ID статьи расходов </param>
        /// <param name="paid"> Оплаченная сумма </param>
        public PatchExpensePaidDto(string expenseId, double paid)
        {
            ExpenseId = expenseId;
            Paid = paid;
        }

        #endregion
    }
}
