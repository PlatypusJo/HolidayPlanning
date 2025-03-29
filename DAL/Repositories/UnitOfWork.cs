using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
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
        /// Контекст FirestoreDb
        /// </summary>
        private readonly FirestoreDb _firestoreDb;

        /// <summary>
        /// Репозиторий Пользователя
        /// </summary>
        private UserRepository _userRepository;

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

        /// <summary>
        /// Репозиторий Гостя
        /// </summary>
        private MemberRepository _memberRepository;

        /// <summary>
        /// Репозиторий Категории гостя
        /// </summary>
        private MemberCategoryRepository _memberCategoryRepository;

        /// <summary>
        /// Репозиторий Категории меню
        /// </summary>
        private MenuCategoryRepository _menuCategoryRepository;

        /// <summary>
        /// Репозиторий Статуса гостя
        /// </summary>
        private MemberStatusRepository _memberStatusRepository;

        /// <summary>
        /// Репозиторий мероприятия для Firestore
        /// </summary>
        private HolidayRepository _holidayFirestoreRep;

        #endregion

        #region Свойства

        public IUserRepository<User> User
        {
            get
            {
                _userRepository ??= new UserRepository(_firestoreDb);
                return _userRepository;
            }
        }

        public IHolidayRepository Holiday
        {
            get
            {
                _holidayRepository ??= new HolidayRepository(_firestoreDb);
                return _holidayRepository;
            }
        }

        public IContractorRepository Contractor
        {
            get
            {
                _contractorRepository ??= new ContractorRepository(_firestoreDb);
                return _contractorRepository;
            }
        }

        public IFrozenCollectionRepository<ContractorCategory> ContractorCategory
        {
            get
            {
                _contractorCategoryRepository ??= new ContractorCategoryRepository(_firestoreDb);
                return _contractorCategoryRepository;
            }
        }
        
        public IFrozenCollectionRepository<ContractorStatus> ContractorStatus
        {
            get
            {
                _contractorStatusRepository ??= new ContractorStatusRepository(_firestoreDb);
                return _contractorStatusRepository;
            }
        }

        public IMemberRepository Member
        {
            get
            {
                _memberRepository ??= new MemberRepository(_firestoreDb);
                return _memberRepository;
            }
        }

        public IFrozenCollectionRepository<MemberCategory> MemberCategory
        {
            get
            {
                _memberCategoryRepository ??= new MemberCategoryRepository(_firestoreDb);
                return _memberCategoryRepository;
            }
        }

        public IFrozenCollectionRepository<MemberStatus> MemberStatus
        {
            get
            {
                _memberStatusRepository ??= new MemberStatusRepository(_firestoreDb);
                return _memberStatusRepository;
            }
        }

        public IFrozenCollectionRepository<MenuCategory> MenuCategory
        {
            get
            {
                _menuCategoryRepository ??= new MenuCategoryRepository(_firestoreDb);
                return _menuCategoryRepository;
            }
        }

        public IRepository<Holiday> HolidayFirestore
        {
            get
            {
                _holidayFirestoreRep ??= new HolidayRepository(_firestoreDb);
                return _holidayFirestoreRep;
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
            _firestoreDb = FirestoreDb.Create("holidayplanning-da398");
        }
        
        #endregion

        #region Методы

        public async Task<int> Save() => 1;
        
        #endregion
    }
}
