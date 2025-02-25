using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс контекста базы данных
    /// </summary>
    public interface IHolidayDbContext
    {
        /// <summary>
        /// Коллекция сущностей Мероприятие
        /// </summary>
        DbSet<Holiday> Holiday { get; set; }
    }
}
