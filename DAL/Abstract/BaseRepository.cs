using DAL.Entities;
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
        protected readonly HolidayPlanningDbContext _db;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конcтруктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public BaseRepository(HolidayPlanningDbContext db) => _db = db;

        #endregion
    }
}
