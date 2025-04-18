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
    /// Сервис Статуса задачи
    /// </summary>
    public class GoalStatusService : BaseService, IGoalStatusService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public GoalStatusService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<GoalStatusDto>> GetAll()
        {
            var items = await _unitOfWork.GoalStatus.GetAll();

            var result = items
                .Select(item => new GoalStatusDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
