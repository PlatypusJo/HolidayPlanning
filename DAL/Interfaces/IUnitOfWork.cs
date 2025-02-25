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
        /// <summary>
        /// Репозиторий Мероприятия
        /// </summary>
        IRepository<Holiday> Holiday { get; }

        /// <summary>
        ///     Сохраняет изменения в бд
        /// </summary>
        /// <returns>
        ///     При успешном выполнени возвращает количество
        ///     записей состояния, записанных в базу данных.
        ///     Иначе возвращает минимальное значение Int32.
        /// </returns>
        Task<int> Save();
    }
}
