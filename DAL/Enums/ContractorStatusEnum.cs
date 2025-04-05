using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    /// <summary>
    /// Перечисление статусов подрядчика
    /// </summary>
    public enum ContractorStatusEnum : byte
    {
        /// <summary>
        /// В ожидании
        /// </summary>
        Wait = 1,

        /// <summary>
        /// Подтверждён
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Отклонён
        /// </summary>
        Refused = 3
    }
}
