using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Гостя
    /// </summary>
    public interface IMemberService : ICrud<MemberDto>
    {
        /// <summary>
        /// Возвращает всех гостей выбранного мероприятия в виде dto
        /// </summary>
        /// <param name="holidayId">ID Мероприятия</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<MemberDto>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет статус гостя
        /// </summary>
        /// <param name="itemDto">dto-класс, содержащий статус гостя и id гостя</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> PatchMemberStatus(PatchMemberStatusDto itemDto);
    }
}
