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
    /// Тесты для конвертора Категории меню
    /// </summary>
    public class MenuCategoryConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"text", "Category title"}
            };
            MenuCategory expected = new()
            {
                Id = "X100",
                Title = "Category title"
            };

            // Act:
            var actual = MenuCategoryConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            MenuCategory model = new()
            {
                Id = "X100",
                Title = "Category title"
            };
            Dictionary<string, object> expected = new()
            {
                {"text", $"{model.Title}"}
            };

            // Act:
            var actual = MenuCategoryConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
