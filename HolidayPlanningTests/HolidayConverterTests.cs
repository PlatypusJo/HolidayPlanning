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
    /// Тесты для конвертера Мероприятия
    /// </summary>
    public class HolidayConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            DateTime startDate = new DateTime(2015, 1, 11, 11, 11, 00);
            DateTime endDate = new DateTime(2015, 1, 11, 11, 12, 00);
            Dictionary<string, object> dictionary = new()
            {
                {"userId", "1"},
                {"title", "Test holiday"},
                {"startDate", startDate},
                {"endDate", endDate},
                {"budget", 125.5},
            };
            Holiday expected = new()
            {
                Id = "X100",
                UserId = "1",
                Title = "Test holiday",
                StartDate = startDate,
                EndDate = endDate,
                Budget = 125.5
            };

            // Act:
            var actual = HolidayConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            DateTime startDate = new DateTime(2015, 1, 11, 11, 11, 00);
            DateTime endDate = new DateTime(2015, 1, 11, 11, 12, 00);
            Holiday model = new()
            {
                Id = "X100",
                UserId = "1",
                Title = "Test holiday",
                StartDate = startDate,
                EndDate = endDate,
                Budget = 125.5
            };
            Dictionary<string, object> expected = new()
            {
                {"userId", model.UserId},
                {"title", $"{model.Title}"},
                {"startDate", model.StartDate.ToString()},
                {"endDate", model.EndDate.ToString()},
                {"budget", model.Budget.ToString()}
            };

            // Act:
            var actual = HolidayConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
