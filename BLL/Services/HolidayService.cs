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
using Google.Cloud.Firestore;
using System.Diagnostics;

namespace BLL.Services
{
    /// <summary>
    /// Сервис Мероприятия
    /// </summary>
    public class HolidayService : BaseService, IHolidayService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public HolidayService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            
        }
        
        #endregion

        #region Методы
        public async Task<bool> Create(HolidayDto itemDto)
        {
            var holiday = new Holiday
            {
                Id = itemDto.Id,
                Title = itemDto.Title,
                StartDate = itemDto.StartDate,
                EndDate = itemDto.EndDate,
                Budget = itemDto.Budget
            };

            await _unitOfWork.Holiday.Create(holiday);
            return await SaveAsync();
        }

        public async Task<bool> Delete(string id)
        {
            if (!await _unitOfWork.Holiday.Exists(id))
                return false;

            List<Member> members = await _unitOfWork.Member.GetAllByHolidayId(id);
            if (members != null)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    await _unitOfWork.Member.Delete(members[i].Id);
                }
            }

            List<Contractor> contractors = await _unitOfWork.Contractor.GetAllByHolidayId(id);
            if (contractors != null)
            {
                for (int i = 0; i < contractors.Count; i++)
                {
                    await _unitOfWork.Contractor.Delete(contractors[i].Id);
                }
            }

            List<Expense> expenses = await _unitOfWork.Expense.GetAllByHolidayId(id);
            if (expenses != null)
            {
                for (int i = 0; i < expenses.Count; i++)
                {
                    await _unitOfWork.Expense.Delete(expenses[i].Id);
                }
            }

            await _unitOfWork.Holiday.Delete(id);
            return await SaveAsync();
        }

        public async Task<bool> Exists(string id) => await _unitOfWork.Holiday.Exists(id);

        public async Task<List<HolidayDto>> GetAll()
        {
            var items = await _unitOfWork.Holiday.GetAll();

            var result = items
                .Select(item => new HolidayDto(item))
                .ToList();

            return result;
        }

        public async Task<List<HolidayDto>> GetAllByUserId(string userId)
        {
            var items = await _unitOfWork.Holiday.GetAllByUserId(userId);

            var result = items
                .Select(item => new HolidayDto(item))
                .ToList();

            return result;
        }

        public async Task<HolidayDto> GetById(string id)
        {
            var item = await _unitOfWork.Holiday.GetItem(id);

            return item == null ? null : new HolidayDto(item);
        }

        public async Task<bool> Update(HolidayDto itemDto)
        {
            if (!await _unitOfWork.Holiday.Exists(itemDto.Id))
                return false;

            Holiday item = await _unitOfWork.Holiday.GetItem(itemDto.Id);

            item.Id = itemDto.Id;
            item.Title = itemDto.Title;
            item.StartDate = itemDto.StartDate;
            item.EndDate = itemDto.EndDate;
            item.Budget = itemDto.Budget;

            await _unitOfWork.Holiday.Update(item);
            return await SaveAsync();
        }
        
        #endregion
    }
}
