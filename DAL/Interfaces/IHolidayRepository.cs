using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория Мероприятия
    /// </summary>
    public interface IHolidayRepository : IRepository<Holiday>
    {
        /// <summary>
        /// Получение всех мероприятий конкретного пошльзователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns></returns>
        Task<List<Holiday>> GetAllByUserId(string userId);
    }
}
