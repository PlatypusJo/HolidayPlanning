using DAL.Converters;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanningTests
{
    /// <summary>
    /// Тесты для конвертора Статуса гостя
    /// </summary>
    public class MemberStatusConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"text", "Category title"}
            };
            MemberStatus expected = new()
            {
                Id = "X100",
                Title = "Category title"
            };

            // Act:
            var actual = MemberStatusConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            MemberStatus model = new()
            {
                Id = "X100",
                Title = "Category title"
            };
            Dictionary<string, object> expected = new()
            {
                {"text", $"{model.Title}"}
            };

            // Act:
            var actual = MemberStatusConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
