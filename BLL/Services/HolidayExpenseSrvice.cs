using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Interfaces;
using DAL.Providers;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Сервис расходов мероприятия
    /// </summary>
    public class HolidayExpenseSrvice : BaseService, IHolidayExpenseService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public HolidayExpenseSrvice(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<HolidayExpenseDto>> GetAll(string holidayId)
        {
            var expenses = await _unitOfWork.Expense.GetAllByHolidayId(holidayId);
            var contactors = (await _unitOfWork.Contractor.GetAllByHolidayId(holidayId))
                .FindAll(c => ContractorStatusProvider.Provide(c.StatusId) is ContractorStatusEnum.Confirmed);

            var result = expenses
                .Select(item => new HolidayExpenseDto(item))
                .ToList();

            result.AddRange(contactors
                .Select(item => new HolidayExpenseDto(item))
                .ToList());

            return result;
        }

        #endregion
    }
}
