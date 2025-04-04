using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Сервис Статьи расходов
    /// </summary>
    public class ExpenseService : BaseService, IExpenseService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<bool> Create(ExpenseDto itemDto)
        {
            var expense = new Expense
            {
                Id = itemDto.Id,
                HolidayId = itemDto.HolidayId,
                Title = itemDto.Title,
                Description = itemDto.Description,
                Amount = itemDto.Amount,
                Paid = itemDto.Paid
            };

            await _unitOfWork.Expense.Create(expense);
            return await SaveAsync();
        }

        public async Task<bool> Delete(string id)
        {
            if (!await _unitOfWork.Expense.Exists(id))
                return false;

            return await _unitOfWork.Expense.Delete(id);
        }

        public async Task<bool> Exists(string id) =>
            await _unitOfWork.Expense.Exists(id);

        public async Task<List<ExpenseDto>> GetAll()
        {
            var items = await _unitOfWork.Expense.GetAll();

            var result = items
                .Select(item => new ExpenseDto(item))
                .ToList();

            return result;
        }

        public async Task<List<ExpenseDto>> GetAllByHolidayId(string holidayId)
        {
            var items = await _unitOfWork.Expense.GetAllByHolidayId(holidayId);

            var result = items
                .Select(item => new ExpenseDto(item))
                .ToList();

            return result;
        }

        public async Task<ExpenseDto> GetById(string id)
        {
            var item = await _unitOfWork.Expense.GetItem(id);

            return item == null ? null : new ExpenseDto(item);
        }

        public async Task<bool> Update(ExpenseDto itemDto)
        {
            if (!await _unitOfWork.Expense.Exists(itemDto.Id))
                return false;

            Expense item = await _unitOfWork.Expense.GetItem(itemDto.Id);

            item.Id = itemDto.Id;
            item.HolidayId = itemDto.HolidayId;
            item.Title = itemDto.Title;
            item.Description = itemDto.Description;
            item.Amount = itemDto.Amount;
            item.Paid = itemDto.Paid;

            await _unitOfWork.Expense.Update(item);
            return await SaveAsync();
        }

        public async Task<bool> PatchPaid(PatchExpensePaidDto itemDto)
        {
            if (!await _unitOfWork.Expense.Exists(itemDto.ExpenseId))
                return false;

            await _unitOfWork.Expense.PatchPaid(itemDto.ExpenseId, itemDto.Paid);
            return await SaveAsync();
        }

        #endregion
    }
}
