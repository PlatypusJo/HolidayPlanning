using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория класса T
    /// </summary>
    /// <typeparam name="T">Тип класса</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Ищет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> Найденная сущность (null, если сущность не найдена)</returns>
        Task<T> GetItem(int id);

        /// <summary>
        /// Добавляет сущность в коллекцию
        /// </summary>
        /// <param name="item">Сущность</param>
        Task Create(T item);

        /// <summary>
        /// Обновляет сущность в коллекции
        /// </summary>
        /// <param name="item">Сущность</param>
        Task Update(T item);

        /// <summary>
        /// Удаляет из коллекции сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>true, если удаление успешно, иначе false</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Проверяет наличие в коллекции сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>true, если сущность существует, иначе false</returns>
        Task<bool> Exists(int id);
    }
}
