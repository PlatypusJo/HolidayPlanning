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
    /// Статический класс-конвертер для сущности Задача, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class GoalConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static Goal FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            Goal model = new Goal()
            {
                Id = modelId,
                HolidayId = GetString(dictionary, "holidayId"),
                GoalStatusId = GetString(dictionary, "goalStatusId"),
                Title = GetString(dictionary, "title"),
                Deadline = GetDateTime(dictionary, "deadline")
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(Goal model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"holidayId", model.HolidayId},
                {"goalStatusId", model.GoalStatusId},
                {"title", $"{model.Title}"},
                {"deadline", model.Deadline.ToString()}
            };
            return dictionary;
        }
    }
}
