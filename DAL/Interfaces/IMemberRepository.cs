using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория Гостя
    /// </summary>
    public interface IMemberRepository : IRepository<Member>
    {
        /// <summary>
        /// Получение всех гостей конкретного мероприятия
        /// </summary>
        /// <param name="holidayId">ID мероприятия</param>
        /// <returns></returns>
        Task<List<Member>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет статус гостя
        /// </summary>
        /// <param name="memberId">Id Гостя</param>
        /// <param name="memberStatusId">Id Статуса гостя</param>
        Task PatchMemberStatus(string memberId, string memberStatusId);
    }
}
