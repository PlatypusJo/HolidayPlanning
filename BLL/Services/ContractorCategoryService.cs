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
    /// Сервис Категории подрядчика
    /// </summary>
    public class ContractorCategoryService : BaseService, IContractorCategoryService
    {
        #region Конструкторы

        /// <summary>
        /// Контрукор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public ContractorCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<ContractorCategoryDto>> GetAll()
        {
            var items = await _unitOfWork.ContractorCategory.GetAll();

            var result = items
                .Select(item => new ContractorCategoryDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
