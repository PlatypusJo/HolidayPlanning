using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Категорий подрядчика
    /// </summary>
    public interface IContractorCategoryService
    {
        /// <summary>
        /// Возвращает все категории подрядчиков в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<ContractorCategoryDto>> GetAll();
    }
}
