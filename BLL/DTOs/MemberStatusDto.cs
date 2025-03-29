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
    /// DTO-класс Статуса гостя
    /// </summary>
    public class MemberStatusDto
    {
        #region Свойства

        /// <summary>
        /// ID Статуса
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        public string? Title { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MemberStatusDto() { }

        /// <summary>
        /// Конструктор на основе оригинальной сущности
        /// </summary>
        /// <param name="memberStatus">Сущность Статус гостя</param>
        public MemberStatusDto(MemberStatus memberStatus)
        {
            Id = memberStatus.Id;
            Title = memberStatus.Title;
        }

        #endregion
    }
}
