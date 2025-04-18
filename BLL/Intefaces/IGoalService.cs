using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Задач
    /// </summary>
    public interface IGoalService : ICrud<GoalDto>
    {
        /// <summary>
        /// Возвращает все задачи выбранного мероприятия в виде dto
        /// </summary>
        /// <param name="holidayId">ID Мероприятия</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<GoalDto>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет статус задачи
        /// </summary>
        /// <param name="itemDto">dto-класс, содержащий статус задачи и id задачи</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> PatchGoalStatus(PatchGoalStatusDto itemDto);
    }
}
