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
    /// DTO-класс Категория меню
    /// </summary>
    public class MenuCategoryDto
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
        public MenuCategoryDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="menuCategory">Сущность Категория меню</param>
        public MenuCategoryDto(MenuCategory menuCategory)
        {
            Id = menuCategory.Id;
            Title = menuCategory.Title;
        }

        #endregion
    }
}
