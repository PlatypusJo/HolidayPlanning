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
    /// Тесты для конвертора Статуса подрядчика
    /// </summary>
    public class ContractorStatusConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"text", "Status title"}
            };
            GoalStatus expected = new()
            {
                Id = "X100",
                Title = "Status title"
            };

            // Act:
            var actual = GoalStatusConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            GoalStatus model = new()
            {
                Id = "X100",
                Title = "Status title"
            };
            Dictionary<string, object> expected = new()
            {
                {"text", $"{model.Title}"}
            };

            // Act:
            var actual = GoalStatusConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
