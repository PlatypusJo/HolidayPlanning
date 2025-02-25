using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Поля
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly HolidayDbContext _db;

        /// <summary>
        /// Репозиторий мероприятия
        /// </summary>
        private HolidayRepository _holidayRepository;
        #endregion

        #region Свойства
        public IRepository<Holiday> Holiday
        {
            get
            {
                _holidayRepository ??= new HolidayRepository(_db);
                return _holidayRepository;
            }
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор, принимающий контекст базы данных
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public UnitOfWork(HolidayDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Методы
        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
        #endregion
    }
}
