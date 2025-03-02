using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Статуса подрядчика
    /// </summary>
    public class ContractorStatusRepository : BaseRepository, IFrozenCollectionRepository<ContractorStatus>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorStatusRepository(HolidayPlanningDbContext db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<ContractorStatus>> GetAll() => await _db.ContractorStatus.ToListAsync();

        #endregion
    }
}
