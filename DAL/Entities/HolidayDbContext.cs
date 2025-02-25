using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class HolidayDbContext : DbContext, IHolidayDbContext
    {
        #region Свойства
        public virtual DbSet<Holiday> Holiday { get; set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Контруктор контекста на основе options
        /// </summary>
        /// <param name="options">Параметры контекста данных</param>
        public HolidayDbContext(DbContextOptions<HolidayDbContext> options) : base(options) 
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
            Holiday.Add(new Holiday
            {
                Id = 1,
                Title = "День мазута",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Budget = 134
            });
            Holiday.Add(new Holiday
            {
                Id = 2,
                Title = "День цемента",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Budget = 12.50
            });
            SaveChanges();
        }
        #endregion
    }
}
