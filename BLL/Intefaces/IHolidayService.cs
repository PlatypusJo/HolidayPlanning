using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Меропирятий
    /// </summary>
    public interface IHolidayService : ICrud<HolidayDto>
    {
        /// <summary>
        /// Возвращает всех подрядчиков выбранного мероприятия в виде dto
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<HolidayDto>> GetAllByUserId(string userId);
    }
}
