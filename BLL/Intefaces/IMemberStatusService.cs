using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Статуса гостя
    /// </summary>
    public interface IMemberStatusService
    {
        /// <summary>
        /// Возвращает все статусы гостя в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<MemberStatusDto>> GetAll();
    }
}
