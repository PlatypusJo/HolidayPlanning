using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория Статьи расходов.
    /// </summary>
    public interface IExpenseRepository : IRepository<Expense>
    {
        /// <summary>
        /// Получение всех статей расходов конкретного мероприятия.
        /// </summary>
        /// <param name="holidayId">ID мероприятия.</param>
        /// <returns>Список всех статей расходов конкретного мероприятия.</returns>
        Task<List<Expense>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет оплаченную сумму статьи расходов.
        /// </summary>
        /// <param name="expenseId">Id статьи расходов.</param>
        /// <param name="paid">Новая оплаченная сумма.</param>
        Task PatchPaid(string expenseId, double paid);
    }
}
