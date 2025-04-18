using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Статус задачи
    /// </summary>
    public interface IGoalStatusService
    {
        /// <summary>
        /// Возвращает все статусы задачи в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<GoalStatusDto>> GetAll();
    }
}
