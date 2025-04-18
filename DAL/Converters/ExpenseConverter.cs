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
    /// Статический класс-конвертер для сущности Статья расходов, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class ExpenseConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static Expense FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            Expense model = new Expense()
            {
                Id =            modelId,
                HolidayId =     GetString(dictionary, "holidayId"),
                Title =         GetString(dictionary, "title"),
                Description =   GetString(dictionary, "description"),
                Amount =        GetDouble(dictionary, "amount"),
                Paid =          GetDouble(dictionary, "paid"),
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(Expense model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"holidayId", $"{model.HolidayId}"},
                {"title", $"{model.Title}"},
                {"description", $"{model.Description}"},
                {"amount", $"{model.Amount}"},
                {"paid", $"{model.Paid}"}
            };
            return dictionary;
        }
    }
}
