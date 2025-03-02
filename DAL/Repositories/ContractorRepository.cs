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
    /// Репозиторий Подрядчика
    /// </summary>
    public class ContractorRepository : BaseRepository, IContractorRepository
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorRepository(HolidayPlanningDbContext db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Contractor item) => await _db.Contractor.AddAsync(item);

        public async Task<bool> Delete(int id)
        {
            var item = await _db.Contractor.FindAsync(id);
            if (item == null)
                return false;

            _db.Contractor.Remove(item);
            return true;
        }

        public async Task<bool> Exists(int id) => await _db.Contractor.AnyAsync(b => b.Id == id);

        public async Task<List<Contractor>> GetAll() => await _db.Contractor.ToListAsync();

        public async Task<List<Contractor>> GetAllByHolidayId(int holidayId) => await _db.Contractor.Where(c => c.HolidayId == holidayId).ToListAsync();

        public async Task<Contractor> GetItem(int id) => await _db.Contractor.FindAsync(id);

        public async Task Update(Contractor item) => _db.Entry(item).State = EntityState.Modified;

        #endregion
    }
}
