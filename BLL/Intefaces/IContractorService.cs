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
        Task<List<ContractorDto>> GetAllByHolidayId(string holidayId);

        /// <summary>
        /// Обновляет статус подрядчика
        /// </summary>
        /// <param name="itemDto">dto-класс, содержащий статус подрячика и id подрядчика</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> PatchContractorStatus(PatchContractorStatusDto itemDto);

        /// <summary>
        /// Обновляет оплаченную сумму услуги подрядчика
        /// </summary>
        /// <param name="itemDto">dto-класс, содержащий оплаченную сумму и id подрядчика</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> PatchPaid(PatchContractorPaidDto itemDto);
    }
}
