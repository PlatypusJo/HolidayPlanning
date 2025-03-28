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
    /// Сервис Статуса гостя
    /// </summary>
    public class MemberStatusService : BaseService, IMemberStatusService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public MemberStatusService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<List<MemberStatusDto>> GetAll()
        {
            var items = await _unitOfWork.MemberStatus.GetAll();

            var result = items
                .Select(item => new MemberStatusDto(item))
                .ToList();

            return result;
        }

        #endregion
    }
}
