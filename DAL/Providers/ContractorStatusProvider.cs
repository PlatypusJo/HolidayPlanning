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

        /// <summary>
        /// Словарь Статусов подрядчика
        /// </summary>
        private static readonly Dictionary<string, ContractorStatusEnum> _dictionary = [];

        #endregion

        #region Методы

        /// <summary>
        /// Регистрирует все Статусы подрядчика в провайдере в виде enum по ключу id
        /// </summary>
        public static void RegisterAll()
        {
            _dictionary.Clear();
            Register("1", ContractorStatusEnum.Wait);
            Register("2", ContractorStatusEnum.Confirmed);
            Register("3", ContractorStatusEnum.Refused);
        }

        /// <summary>
        /// Регистрирует Статус подрядчика в провайдере в виде enum по ключу id
        /// </summary>
        /// <param name="id">Id Статуса подрядчика</param>
        /// <param name="status">enum Статуса подрядчика</param>
        public static void Register(string id, ContractorStatusEnum status)
        {
            _dictionary.Add(id, status);
        }

        /// <summary>
        /// Получение Статуса подрядчика в виде enum
        /// </summary>
        /// <param name="id">Id Статуса подрядчика</param>
        /// <returns></returns>
        public static ContractorStatusEnum Provide(string id)
        {
            return _dictionary[id];
        }

        #endregion
    }
}
