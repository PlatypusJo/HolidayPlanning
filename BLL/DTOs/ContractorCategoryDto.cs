using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    /// <summary>
    /// DTO-класс Категория подрядчика
    /// </summary>
    public class ContractorCategoryDto
    {
        #region Свойства

        /// <summary>
        /// ID Категории
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string? Title { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ContractorCategoryDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="contractorCategory">Сущность Категория подрядчика</param>
        public ContractorCategoryDto(ContractorCategory contractorCategory)
        {
            Id = contractorCategory.Id;
            Title = contractorCategory.Title;
        }

        #endregion
    }
}
