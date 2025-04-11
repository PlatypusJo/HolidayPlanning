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
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static string GetString(Dictionary<string, object> dictionary, string key, string defaultValue = "")
        {
            string result = defaultValue;
            if (dictionary.ContainsKey(key))
            {
                result = dictionary[key].ToString();
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа bool из словаря по ключу
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        static protected bool GetBool(Dictionary<string, object> dictionary, string key, bool defaultValue = default)
        {
            bool result = defaultValue;
            if (dictionary.ContainsKey(key))
            {
                result = Convert.ToBoolean(dictionary[key].ToString());
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа double из словаря по ключу
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static double GetDouble(Dictionary<string, object> dictionary, string key, double defaultValue = default)
        {
            double result = defaultValue;
            if (dictionary.ContainsKey(key))
            {
                result = Convert.ToDouble(dictionary[key].ToString());
            }
            return result;
        }

        /// <summary>
        /// Получение значения типа double из словаря по ключу
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение, возвращаемое по умолчанию</param>
        /// <returns>Возвращает найденное значение. Если значение по ключу не найдено - вовзращает значение по умолчанию</returns>
        protected static DateTime GetDateTime(Dictionary<string, object> dictionary, string key, DateTime defaultValue = default)
        {
            DateTime result = defaultValue;
            if (dictionary.ContainsKey(key))
            {
                result = DateTime.Parse(dictionary[key].ToString());
            }
            return result;
        }

        #endregion

    }
}
