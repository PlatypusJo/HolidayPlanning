using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Сущность Категория подрядчика (захардкоженный справочник)
    /// </summary>
    public class ContractorCategory
    {
        #region Свойства

        /// <summary>
        /// ID Категории
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Подрядчики категории
        /// </summary>
        public virtual ICollection<Contractor> Contractors { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ContractorCategory()
        {
            Contractors = new HashSet<Contractor>();
        }

        #endregion
    }
}
