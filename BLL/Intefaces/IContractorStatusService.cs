using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Статус подрядчика
    /// </summary>
    public interface IContractorStatusService
    {
        /// <summary>
        /// Возвращает все статусы подрядчиков в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<ContractorStatusDto>> GetAll();
    }
}
