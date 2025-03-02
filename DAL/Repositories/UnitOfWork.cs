using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Класс, реализующий паттерн UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Поля

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly HolidayPlanningDbContext _db;

        /// <summary>
        /// Репозиторий Мероприятия
        /// </summary>
        private HolidayRepository _holidayRepository;

        /// <summary>
        /// Репозиторий Подрядчика
        /// </summary>
        private ContractorRepository _contractorRepository;

        /// <summary>
        /// Репозиторий Категории подрядчика
        /// </summary>
        private ContractorCategoryRepository _contractorCategoryRepository;

        /// <summary>
        /// Репозиторий Статуса подрядчика
        /// </summary>
        private ContractorStatusRepository _contractorStatusRepository;

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

        public IContractorRepository Contractor
        {
            get
            {
                _contractorRepository ??= new ContractorRepository(_db);
                return _contractorRepository;
            }
        }

        public IFrozenCollectionRepository<ContractorCategory> ContractorCategory
        {
            get
            {
                _contractorCategoryRepository ??= new ContractorCategoryRepository(_db);
                return _contractorCategoryRepository;
            }
        }
        
        public IFrozenCollectionRepository<ContractorStatus> ContractorStatus
        {
            get
            {
                _contractorStatusRepository ??= new ContractorStatusRepository(_db);
                return _contractorStatusRepository;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор, принимающий контекст базы данных
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public UnitOfWork(HolidayPlanningDbContext db)
        {
            _db = db;
        }
        
        #endregion

        #region Методы

        public async Task<int> Save() => await _db.SaveChangesAsync();
        
        #endregion
    }
}
