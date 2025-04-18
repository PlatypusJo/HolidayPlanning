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
    /// Сервис Задачи
    /// </summary>
    public class GoalService : BaseService, IGoalService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public GoalService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<bool> Create(GoalDto itemDto)
        {
            var goal = new Goal
            {
                Id = itemDto.Id,
                HolidayId = itemDto.HolidayId,
                GoalStatusId = itemDto.GoalStatusId,
                Title = itemDto.Title,
                Deadline = itemDto.Deadline
            };

            await _unitOfWork.Goal.Create(goal);
            return await SaveAsync();
        }

        public async Task<bool> Delete(string id)
        {
            if (!await _unitOfWork.Goal.Exists(id))
                return false;

            await _unitOfWork.Goal.Delete(id);
            return await SaveAsync();
        }

        public async Task<bool> Exists(string id) => await _unitOfWork.Goal.Exists(id);

        public async Task<List<GoalDto>> GetAll()
        {
            var items = await _unitOfWork.Goal.GetAll();

            var result = items
                .Select(item => new GoalDto(item))
                .ToList();

            return result;
        }

        public async Task<List<GoalDto>> GetAllByHolidayId(string holidayId)
        {
            var items = await _unitOfWork.Goal.GetAllByHolidayId(holidayId);

            var result = items
                .Select(item => new GoalDto(item))
                .ToList();

            return result;
        }

        public async Task<GoalDto> GetById(string id)
        {
            var item = await _unitOfWork.Goal.GetItem(id);

            return item == null ? null : new GoalDto(item);
        }

        public async Task<bool> Update(GoalDto itemDto)
        {
            if (!await _unitOfWork.Goal.Exists(itemDto.Id))
                return false;

            Goal item = await _unitOfWork.Goal.GetItem(itemDto.Id);

            item.Id = itemDto.Id;
            item.HolidayId = itemDto.HolidayId;
            item.GoalStatusId = itemDto.GoalStatusId;
            item.Title = itemDto.Title;
            item.Deadline = itemDto.Deadline;

            await _unitOfWork.Goal.Update(item);
            return await SaveAsync();
        }

        public async Task<bool> PatchGoalStatus(PatchGoalStatusDto itemDto)
        {
            if (!await _unitOfWork.Goal.Exists(itemDto.GoalId))
                return false;

            await _unitOfWork.Goal.PatchGoalStatus(itemDto.GoalId, itemDto.GoalStatusId);
            return await SaveAsync();
        }

        #endregion
    }
}
