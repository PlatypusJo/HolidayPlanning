using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IHolidayRepositoryFirestore<T> where T : class
    {
        /// <summary>
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Добавляет сущность в коллекцию
        /// </summary>
        /// <param name="item">Сущность</param>
        Task Create(T item);

        /// <summary>
        /// Обновляет сущность в коллекции
        /// </summary>
        /// <param name="item">Сущность</param>
        Task<WriteResult> Update(T item);

        /// <summary>
        /// Удаляет из коллекции сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>true, если удаление успешно, иначе false</returns>
        Task<WriteResult> Delete(string id);
    }
}
