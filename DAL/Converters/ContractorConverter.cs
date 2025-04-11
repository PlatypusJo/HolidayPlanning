using DAL.Abstract;
using DAL.Entities;

namespace DAL.Converters
{
    /// <summary>
    /// Статический класс-конвертер для сущности Подрядчик, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class ContractorConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static Contractor FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            Contractor model = new Contractor()
            {
                Id =            modelId,
                Title =         GetString(dictionary, "title"),
                СategoryId =    GetString(dictionary, "categoryId"),
                StatusId =      GetString(dictionary, "statusId"),
                HolidayId =     GetString(dictionary, "holidayId"),
                Description =   GetString(dictionary, "description"),
                Email =         GetString(dictionary, "email"),
                PhoneNumber =   GetString(dictionary, "phoneNumber"),
                ServiceCost =   GetDouble(dictionary, "serviceCost"),
                Paid =          GetDouble(dictionary, "paid"),
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(Contractor model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"title", $"{model.Title}"},
                {"description", $"{model.Description}"},
                {"email", $"{model.Email}"},
                {"phoneNumber", $"{model.PhoneNumber}"},
                {"serviceCost", $"{model.ServiceCost}"},
                {"paid", $"{model.Paid}"},
                {"categoryId", $"{model.СategoryId}"},
                {"statusId", $"{model.StatusId}"},
                {"holidayId", $"{model.HolidayId}"}
            };

            return dictionary;
        }
    }
}
