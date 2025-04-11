using DAL.Abstract;
using DAL.Entities;
using Google.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.Converters
{
    /// <summary>
    /// Статический класс-конвертер для сущности Категория подрядчика, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class MemberConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static Member FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            Member model = new Member()
            {
                Id =                modelId,
                HolidayId =         GetString(dictionary, "holidayId"),
                MemberCategoryId =  GetString(dictionary, "memberCategoryId"),
                MemberStatusId =    GetString(dictionary, "memberStatusId"),
                MenuCategoryId =    GetString(dictionary, "menuCategoryId"),
                FIO =               GetString(dictionary, "fio"),
                PhoneNumber =       GetString(dictionary, "phoneNumber"),
                Email =             GetString(dictionary, "email"),
                Comment =           GetString(dictionary, "comment"),
                IsChild =           GetBool(dictionary, "isChild"),
                IsMale =            GetBool(dictionary, "isMale"),
                Seat =              GetString(dictionary, "seat"),
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(Member model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"holidayId", $"{model.HolidayId}"},
                {"memberCategoryId", $"{model.MemberCategoryId}"},
                {"memberStatusId", $"{model.MemberStatusId}"},
                {"menuCategoryId", $"{model.MenuCategoryId}"},
                {"fio", $"{model.FIO}"},
                {"phoneNumber", $"{model.PhoneNumber}"},
                {"email", $"{model.Email}"},
                {"comment", $"{model.Comment}"},
                {"isChild", $"{model.IsChild}"},
                {"isMale", $"{model.IsMale}"},
                {"seat", $"{model.Seat}"},
            };
            return dictionary;
        }
    }
}
