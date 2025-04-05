using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Статьи расходов
    /// </summary>
    public interface IExpenseService : ICrud<ExpenseDto>
    {
        /// <summary>
        /// Возвращает все статьи расходов выбранного мероприятия в виде dto
        /// </summary>
        /// <param name="holidayId">ID Мероприятия</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<ExpenseDto>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет оплаченную сумму статьи расхода
        /// </summary>
        /// <param name="itemDto">dto-класс, содержащий оплаченную сумму и id статьи расходов</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> PatchPaid(PatchExpensePaidDto itemDto);
    }
}
