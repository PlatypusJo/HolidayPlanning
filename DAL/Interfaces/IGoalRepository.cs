using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория Задачи
    /// </summary>
    public interface IGoalRepository : IRepository<Goal>
    {
        /// <summary>
        /// Получение всех задач конкретного мероприятия
        /// </summary>
        /// <param name="holidayId">ID мероприятия</param>
        /// <returns></returns>
        Task<List<Goal>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновление статуса задачи
        /// </summary>
        /// <param name="goalId">Id Задачи</param>
        /// <param name="goalStatusId">Id Статуса задачи</param>
        Task PatchGoalStatus(string goalId, string goalStatusId);
    }
}
