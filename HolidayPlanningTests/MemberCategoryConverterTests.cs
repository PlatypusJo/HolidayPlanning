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
    /// Тесты для конвертора Категории гостя
    /// </summary>
    public class MemberCategoryConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"text", "Category title"}
            };
            MemberCategory expected = new()
            {
                Id = "X100",
                Title = "Category title"
            };

            // Act:
            var actual = MemberCategoryConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            MemberCategory model = new()
            {
                Id = "X100",
                Title = "Category title"
            };
            Dictionary<string, object> expected = new()
            {
                {"text", $"{model.Title}"}
            };

            // Act:
            var actual = MemberCategoryConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
