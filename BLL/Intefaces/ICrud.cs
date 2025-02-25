using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Intefaces
{
    /// <summary>
    /// Интерфейс для реализации CRUD операций
    /// </summary>
    /// <typeparam name="T">Тип класса</typeparam>
    public interface ICrud<T> where T : class
    {
        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="itemDto">DTO сущности</param>
        /// <returns>
        ///     Возращает true при успешном создании экземпляра сущности T,
        ///     иначе false
        /// </returns>
        Task<bool> Create(T itemDto);

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> Возращает true при успешном удалении экзмпляра сущности T, иначе false </returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Проверяет существование сущности с заданным ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Возращает true, если экземпляр сущности с указанным id существует, иначе false </returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Возвращает все экземпляры сущности
        /// </summary>
        /// <returns>Список сущностей</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Возвращает экземпляр сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность (null, если сущность не найдена)</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="itemDto">Заданная сущности</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> Update(T itemDto);
    }
}
