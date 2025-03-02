using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Мероприятия
    /// </summary>
    public class HolidayRepository : BaseRepository, IRepository<Holiday>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public HolidayRepository(HolidayPlanningDbContext db) : base(db) { }
        
        #endregion

        #region Методы

        public async Task Create(Holiday item) => await _db.Holiday.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var item = await _db.Holiday.FindAsync(id);
            if (item == null)
                return false;

            _db.Holiday.Remove(item);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Holiday.AnyAsync(b => b.Id == id);

        public async Task<List<Holiday>> GetAll() => await _db.Holiday.ToListAsync();

        public async Task<Holiday> GetItem(int id) => await _db.Holiday.FindAsync(id);

        public async Task Update(Holiday item) => _db.Entry(item).State = EntityState.Modified;
        
        #endregion
    }
}
