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
    /// Тесты для конвертора Задачи
    /// </summary>
    public class GoalConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            DateTime deadline = new DateTime(2015, 1, 11, 11, 11, 00);
            Dictionary<string, object> dictionary = new()
            {
                {"holidayId", "1"},
                {"goalStatusId", "1"},
                {"title", "Title"},
                {"deadline", deadline}
            };
            Goal expected = new()
            {
                Id = "X100",
                HolidayId = "1",
                GoalStatusId = "1",
                Title = "Title",
                Deadline = deadline
            };

            // Act:
            var actual = GoalConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            DateTime deadline = new DateTime(2015, 1, 11, 11, 11, 00);
            Goal model = new()
            {
                Id = "X100",
                HolidayId = "1",
                GoalStatusId = "1",
                Title = "Title",
                Deadline = deadline
            };
            Dictionary<string, object> expected = new()
            {
                {"holidayId", model.HolidayId},
                {"goalStatusId", model.GoalStatusId},
                {"title", $"{model.Title}"},
                {"deadline", model.Deadline.ToString()}
            };

            // Act:
            var actual = GoalConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
