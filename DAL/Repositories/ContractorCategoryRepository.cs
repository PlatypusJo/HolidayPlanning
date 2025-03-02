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
    /// Репозиторий Категории подрядчика
    /// </summary>
    public class ContractorCategoryRepository : BaseRepository, IFrozenCollectionRepository<ContractorCategory>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorCategoryRepository(HolidayPlanningDbContext db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<ContractorCategory>> GetAll() => await _db.ContractorCategory.ToListAsync();

        #endregion
    }
}
