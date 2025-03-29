using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary> 
    /// Интерфейс для реализации паттерна UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        #region Репозитории

        /// <summary>
        /// Репозиторий Мероприятия
        /// </summary>
        IRepository<Holiday> Holiday { get; }

        /// <summary>
        /// Репозиторий Пользователя
        /// </summary>
        IUserRepository<User> User { get; }

        /// <summary>
        /// Репозиторий Подрядчика
        /// </summary>
        IContractorRepository Contractor { get; }

        /// <summary>
        /// Репозиторий Категории подрядчика
        /// </summary>
        IFrozenCollectionRepository<ContractorCategory> ContractorCategory { get; }

        /// <summary>
        /// Репозиторий Статуса подрядчика
        /// </summary>
        IFrozenCollectionRepository<ContractorStatus> ContractorStatus { get; }

        /// <summary>
        /// Репозиторий Гостя
        /// </summary>
        IMemberRepository Member { get; }

        /// <summary>
        /// Репозиторий Категории гостя
        /// </summary>
        IFrozenCollectionRepository<MemberCategory> MemberCategory { get; }

        /// <summary>
        /// Репозиторий Статуса гостя
        /// </summary>
        IFrozenCollectionRepository<MemberStatus> MemberStatus { get; }

        /// <summary>
        /// Репозиторий Категории меню
        /// </summary>
        IFrozenCollectionRepository<MenuCategory> MenuCategory { get; }

        /// <summary>
        /// Репозиторий мероприятия для Firestore
        /// </summary>
        IRepository<Holiday> HolidayFirestore { get; }

        #endregion

        #region Методы

        /// <summary>
        ///     Сохраняет изменения в бд
        /// </summary>
        /// <returns>
        ///     При успешном выполнении возвращает количество
        ///     записей состояния, записанных в базу данных.
        ///     Иначе возвращает минимальное значение Int32.
        /// </returns>
        Task<int> Save();

        #endregion
    }
}
