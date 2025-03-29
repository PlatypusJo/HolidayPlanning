using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для сервиса Категорий меню
    /// </summary>
    public interface IMenuCategoryService
    {
        /// <summary>
        /// Возвращает все категории меню в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<MenuCategoryDto>> GetAll();
    }
}
