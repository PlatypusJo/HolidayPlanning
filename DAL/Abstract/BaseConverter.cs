using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    /// <summary>
    /// Базовый класс конвертеров. Содержит вспомогательные методы преобразования отдельных типов данных
    /// </summary>
    public class BaseConverter
    {

        #region Внутренние методы

        /// <summary>
        /// Получение значения типа string из словаря по ключу
        /// </summary>
        /// <param name="dictionaty">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static string GetString(Dictionary<string, object> dictionaty, string key, string defaultValue = "")
        {
            string result = defaultValue;
            if (dictionaty.ContainsKey(key))
            {
                result = dictionaty[key].ToString();
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа bool из словаря по ключу
        /// </summary>
        /// <param name="dictionaty">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        static protected bool GetBool(Dictionary<string, object> dictionaty, string key, bool defaultValue = default)
        {
            bool result = defaultValue;
            if (dictionaty.ContainsKey(key))
            {
                result = Convert.ToBoolean(dictionaty[key].ToString());
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа double из словаря по ключу
        /// </summary>
        /// <param name="dictionaty">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static double GetDouble(Dictionary<string, object> dictionaty, string key, double defaultValue = default)
        {
            double result = defaultValue;
            if (dictionaty.ContainsKey(key))
            {
                result = Convert.ToDouble(dictionaty[key].ToString());
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа double из словаря по ключу
        /// </summary>
        /// <param name="dictionaty">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static DateTime GetDateTime(Dictionary<string, object> dictionaty, string key, DateTime defaultValue = default)
        {
            DateTime result = defaultValue;
            if (dictionaty.ContainsKey(key))
            {
                result = DateTime.Parse(dictionaty[key].ToString());
            }
            return result;
        }

        #endregion

    }
}
