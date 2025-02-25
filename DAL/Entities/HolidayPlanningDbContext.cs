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
    public class HolidayPlanningDbContext : DbContext
    {
        #region Свойства

        public virtual DbSet<Holiday> Holiday { get; set; }
        
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
            SaveChanges();
        }
        
        #endregion
    }
}
