﻿using System;
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
        Task<bool> Delete(string id);

        /// <summary>
        /// Проверяет существование сущности с заданным ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Возращает true, если экземпляр сущности с указанным id существует, иначе false </returns>
        Task<bool> Exists(string id);

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        Task<T> GetById(string id);

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="itemDto">Заданная сущности в форме dto</param>
        /// <returns>true при успешном обновлении экземпляра сущности, иначе false</returns>
        Task<bool> Update(T itemDto);
    }
}
