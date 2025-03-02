using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class HolidayPlanningDbContext : DbContext
    {
        #region Свойства

        /// <summary>
        /// Мероприятия
        /// </summary>
        public virtual DbSet<Holiday> Holiday { get; set; }

        /// <summary>
        /// Подрядчики
        /// </summary>
        public virtual DbSet<Contractor> Contractor { get; set; }

        /// <summary>
        /// Категории подрядчиков
        /// </summary>
        public virtual DbSet<ContractorCategory> ContractorCategory { get; set; }

        /// <summary>
        /// Статусы подрядчиков
        /// </summary>
        public virtual DbSet<ContractorStatus> ContractorStatus { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор контекста на основе options
        /// </summary>
        /// <param name="options">Параметры контекста данных</param>
        public HolidayPlanningDbContext(DbContextOptions<HolidayPlanningDbContext> options) : base(options) 
        {
            InitializeMockData();
        }
        
        #endregion

        #region Внутренние методы

        /// <summary>
        /// Инициализирует мокнутую бд начальными значениями
        /// </summary>
        private void InitializeMockData()
        {
            #region Категории Подрядчиков

            ContractorCategory.Add(new ContractorCategory{ Id = 1, Title = "Одежда&Аксессуары" });
            ContractorCategory.Add(new ContractorCategory{ Id = 2, Title = "Красота&Здоровье" });
            ContractorCategory.Add(new ContractorCategory{ Id = 3, Title = "Музыка&Шоу" });
            ContractorCategory.Add(new ContractorCategory{ Id = 4, Title = "Цветы&Декор" });
            ContractorCategory.Add(new ContractorCategory{ Id = 5, Title = "Фото&Видео" });
            ContractorCategory.Add(new ContractorCategory{ Id = 6, Title = "Банкет" });
            ContractorCategory.Add(new ContractorCategory{ Id = 7, Title = "Ведущие" });
            ContractorCategory.Add(new ContractorCategory{ Id = 8, Title = "Транспорт" });
            ContractorCategory.Add(new ContractorCategory{ Id = 9, Title = "Жилье" });

            #endregion

            #region Статусы подрядчиков

            ContractorStatus.Add(new ContractorStatus{ Id = 1, Title = "Ожидание" });
            ContractorStatus.Add(new ContractorStatus{ Id = 2, Title = "Принял" });
            ContractorStatus.Add(new ContractorStatus{ Id = 3, Title = "Отклонил" });

            #endregion

            #region Мероприятия

            Holiday.Add(new Holiday
            {
                Id = 1,
                Title = "День мазута",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Budget = 134
            });
            Holiday.Add(new Holiday
            {
                Id = 2,
                Title = "День цемента",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Budget = 12.50
            });

            #endregion

            #region Подрядчики

            Contractor.Add(new Contractor
            {
                Id = 1,
                HolidayId = 1,
                StatusId = 1,
                СategoryId = 5,
                Title = "Смирнов Л.А.",
                Description = "Фотограф, оператор",
                PhoneNumber = "+71112223344",
                Email = "levaS@mail.ru",
                ServiceCost = 1500,
            });
            Contractor.Add(new Contractor
            {
                Id = 2,
                HolidayId = 1,
                StatusId = 2,
                СategoryId = 1,
                Title = "Самойлова Н.Н.",
                Description = "Реквизитор, стилист",
                PhoneNumber = "+81112223344",
                Email = "nns@yandex.ru",
                ServiceCost = 4000,
            });
            Contractor.Add(new Contractor
            {
                Id = 3,
                HolidayId = 2,
                StatusId = 1,
                СategoryId = 8,
                Title = "Михалков А.Р.",
                Description = "Водитель, грузчик",
                PhoneNumber = "+91112223344",
                Email = "miha@mail.ru",
                ServiceCost = 1300,
            });
            Contractor.Add(new Contractor
            {
                Id = 4,
                HolidayId = 2,
                StatusId = 3,
                СategoryId = 7,
                Title = "Можайский И.О.",
                Description = "Ведущий, тамада",
                PhoneNumber = "+61112223344",
                Email = "moja@mail.ru",
                ServiceCost = 14000,
            });

            #endregion

            SaveChanges();
        }
        
        #endregion
    }
}
