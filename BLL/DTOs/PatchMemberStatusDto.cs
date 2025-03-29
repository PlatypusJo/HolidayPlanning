using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    /// <summary>
    /// Dto-класс для изменения статуса гостя в сущности гостя методом Patch
    /// </summary>
    public class PatchMemberStatusDto
    {
        #region Свойства

        /// <summary>
        /// ID Гостя
        /// </summary>
        [Key]
        public string MemberId { get; set; }

        /// <summary>
        /// ID Статуса гостя
        /// </summary>
        public string MemberStatusId { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PatchMemberStatusDto() { }

        /// <summary>
        /// Конструктор на основе ID Гостя и ID Статуса гостя
        /// </summary>
        /// <param name="memberStatusId">ID Статуса гостя</param>
        /// <param name="memberId">ID Гостя</param>
        public PatchMemberStatusDto(string memberId, string memberStatusId)
        {
            MemberId = memberId;
            MemberStatusId = memberStatusId;
        }

        #endregion
    }
}
