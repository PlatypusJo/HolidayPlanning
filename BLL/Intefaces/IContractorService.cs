using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Подрядчиков
    /// </summary>
    public interface IContractorService : ICrud<ContractorDto>
    {
        /// <summary>
        /// Возвращает всех подрядчиков выбранного мероприятия в виде dto
        /// </summary>
        /// <param name="holidayId">ID Мероприятия</param>
        /// <returns>Список dto сущностей</returns>
        Task<List<ContractorDto>> GetAllByHolidayId(int holidayId);
    }
}
