using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса всех расходов мероприятия
    /// </summary>
    public interface IHolidayExpenseService
    {
        /// <summary>
        /// Получить все расходы мероприятия в виде dto
        /// </summary>
        /// <param name="holidayId">ID мероприятия</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<HolidayExpenseDto>> GetAll(string holidayId);
    }
}
