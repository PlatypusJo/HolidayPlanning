using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Сервис Категории меню
    /// </summary>
    public class MenuCategoryService : BaseService, IMenuCategoryService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public MenuCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<MenuCategoryDto>> GetAll()
        {
            var items = await _unitOfWork.MenuCategory.GetAll();

            var result = items
                .Select(item => new MenuCategoryDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
