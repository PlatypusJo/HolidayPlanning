﻿using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Converters
{
    /// <summary>
    /// Статический класс-конвертер для сущности Статус гостя, для передачи данных в Firebase и принятия данных из него
    /// </summary>
    public class MemberStatusConverter : BaseConverter
    {
        /// <summary>
        /// Конвертер из словаря, собранного на основе документа из firebse, в класс модели
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="modelId">Id модели</param>
        /// <returns>Заполненный из словаря класс модели</returns>
        public static MemberStatus FromDictionaryToModel(Dictionary<string, object> dictionary, string modelId)
        {
            MemberStatus model = new MemberStatus()
            {
                Id = modelId,
                Title = GetString(dictionary, "text"),
            };
            return model;
        }

        /// <summary>
        /// Конвертер из класса модели в словарь, собранный для отправки данных в firebse
        /// </summary>
        /// <param name="model">Класс модели</param>
        /// <returns>Словарь, заполненный на основве класса модели</returns>
        public static Dictionary<string, object> FromModelToDictionary(MemberStatus model)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {"text", $"{model.Title}"}
            };
            return dictionary;
        }
    }
}
