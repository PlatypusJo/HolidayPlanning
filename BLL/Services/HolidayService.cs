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
    /// Сервис Мероприятия
    /// </summary>
    public class HolidayService : BaseService, IHolidayService
    {
        #region Конструкторы

        /// <summary>
        /// Контрукор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public HolidayService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        
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

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.Holiday.Exists(id))
                return false;

            await _unitOfWork.Holiday.Delete(id);
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.Holiday.Exists(id);

        public async Task<List<HolidayDto>> GetAll()
        {
            var items = await _unitOfWork.Holiday.GetAll();

            var result = items
                .Select(item => new HolidayDto(item))
                .ToList();

            return result;
        }

        public async Task<HolidayDto> GetById(int id)
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
