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
    /// Сервис Категории гостя
    /// </summary>
    public class MemberCategoryService : BaseService, IMemberCategoryService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public MemberCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<MemberCategoryDto>> GetAll()
        {
            var items = await _unitOfWork.MemberCategory.GetAll();

            var result = items
                .Select(item => new MemberCategoryDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
