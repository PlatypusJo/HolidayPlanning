using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Статус подрядчика (захардкоженный справочник)
    /// </summary>
    public class ContractorStatus
    {
        #region Свойства

        /// <summary>
        /// ID Статуса
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Подрядчики статуса
        /// </summary>
        public virtual ICollection<Contractor> Contractors { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ContractorStatus()
        {
            Contractors = new HashSet<Contractor>();
        }

        #endregion
    }
}
