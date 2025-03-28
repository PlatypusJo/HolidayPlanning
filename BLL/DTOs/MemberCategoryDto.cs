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
    /// DTO-класс Категория гостя
    /// </summary>
    public class MemberCategoryDto
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
        public MemberCategoryDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="memberCategory">Сущность Категория гостя</param>
        public MemberCategoryDto(MemberCategory memberCategory)
        {
            Id = memberCategory.Id;
            Title = memberCategory.Title;
        }

        #endregion
    }
}
