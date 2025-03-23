using DAL.Entities;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    /// <summary>
    /// Суперкласс для репозиториев
    /// </summary>
    public abstract class BaseRepository
    {
        #region Поля

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        protected readonly FirestoreDb _db;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конcтруктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public BaseRepository(FirestoreDb db) => _db = db;

        #endregion
    }
}
