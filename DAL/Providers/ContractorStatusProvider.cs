using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Providers
{
    /// <summary>
    /// Провайдер статусов подрядчика
    /// </summary>
    public static class ContractorStatusProvider
    {
        #region Поля

        private static readonly Dictionary<string, ContractorStatusEnum> _dictionary = [];

        #endregion

        #region Методы

        public static void RegisterAll()
        {
            _dictionary.Clear();
            Register("1", ContractorStatusEnum.Wait);
            Register("2", ContractorStatusEnum.Confirmed);
            Register("3", ContractorStatusEnum.Refused);
        }

        public static void Register(string name, ContractorStatusEnum status)
        {
            _dictionary.Add(name, status);
        }

        public static ContractorStatusEnum Provide(string id)
        {
            return _dictionary[id];
        }

        #endregion
    }
}
