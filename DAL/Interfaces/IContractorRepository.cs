using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория Подрядчика
    /// </summary>
    public interface IContractorRepository : IRepository<Contractor>
    {
        /// <summary>
        /// Получение всех подрядчиков конкретного мероприятия
        /// </summary>
        /// <param name="holidayId">ID мероприятия</param>
        /// <returns></returns>
        Task<List<Contractor>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет статус подрядчика
        /// </summary>
        /// <param name="contractorId">Id Подрядчика</param>
        /// <param name="contractorStatusId">Id Статуса подрядчика</param>
        Task PatchContractorStatus(string contractorId, string contractorStatusId);
    }
}
