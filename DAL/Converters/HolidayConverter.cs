using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Converters
{
    /// <summary>
    /// Статический класс-конвертер для сущности Мероприятие, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class HolidayConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static Holiday FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            Holiday model = new Holiday()
            {
                Id =            modelId,
                UserId =        GetString(dictionary, "userId"),
                Title =         GetString(dictionary, "title"),
                StartDate =     GetDateTime(dictionary, "startDate"),
                EndDate =       GetDateTime(dictionary, "endDate"),
                Budget =        GetDouble(dictionary, "budget"),
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(Holiday model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"userId", model.UserId},
                {"title", $"{model.Title}"},
                {"startDate", model.StartDate.ToString()},
                {"endDate", model.EndDate.ToString()},
                {"budget", model.Budget.ToString()}
            };
            return dictionary;
        }
    }
}
