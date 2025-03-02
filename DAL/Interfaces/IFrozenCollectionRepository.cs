using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для справочников, содержимое которых не будет изменятся в процессе работы приложения
    /// </summary>
    /// <typeparam name="T">Тип справочника</typeparam>
    public interface IFrozenCollectionRepository<T> where T : class
    {
        /// <summary>
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<List<T>> GetAll();
    }
}
