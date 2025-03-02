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
    /// Сервис Статуса подрядчика
    /// </summary>
    public class ContractorStatusService : BaseService, IContractorStatusService
    {
        #region Конструкторы

        /// <summary>
        /// Контрукор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public ContractorStatusService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<ContractorStatusDto>> GetAll()
        {
            var items = await _unitOfWork.ContractorStatus.GetAll();

            var result = items
                .Select(item => new ContractorStatusDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
