using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Категорий гостя
    /// </summary>
    public interface IMemberCategoryService
    {
        /// <summary>
        /// Возвращает все категории гостя в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<MemberCategoryDto>> GetAll();
    }
}
