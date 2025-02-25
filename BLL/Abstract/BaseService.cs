using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    /// <summary>
    /// Суперкласс для всех сервисов
    /// </summary>
    public abstract class BaseService
    {
        #region Поля
        /// <summary>
        /// Экзепляр класса UnitOfWork
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Конструкторы
        /// <summary>
        /// Контрукор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        protected BaseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        #endregion

        #region Внутренние методы
        /// <summary>
        ///     Сохраняет изменения в бд
        /// </summary>
        /// <returns>
        ///     true при успешном выполнении, иначе false
        /// </returns>
        protected virtual async Task<bool> SaveAsync() => await _unitOfWork.Save() > 0;
        #endregion
    }
}
