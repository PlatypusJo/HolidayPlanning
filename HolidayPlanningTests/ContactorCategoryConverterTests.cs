using DAL.Converters;
using DAL.Entities;

namespace HolidayPlanningTests
{
    /// <summary>
    /// Тесты для конвертора Категории Подрядчика
    /// </summary>
    public class ContactorCategoryConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"text", "Category title"}
            };
            ContractorCategory expected = new()
            {
                Id = "X100",
                Title = "Category title"
            };

            // Act:
            var actual = ContractorCategoryConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            ContractorCategory model = new()
            {
                Id = "X100",
                Title = "Category title"
            };
            Dictionary<string, object> expected = new()
            {
                {"text", $"{model.Title}"}
            };

            // Act:
            var actual = ContractorCategoryConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}