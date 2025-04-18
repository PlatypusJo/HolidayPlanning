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
    /// 
    /// </summary>
    public class ExpenseConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"holidayId", "1"},
                {"title", "Title"},
                {"description", "Desription"},
                {"amount", 100},
                {"paid", 10}
            };
            Expense expected = new Expense()
            {
                Id = "X100",
                HolidayId = "1",
                Title = "Title",
                Description = "Desription",
                Amount = 100,
                Paid = 10,
            };

            // Act:
            var actual = ExpenseConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            Expense model = new()
            {
                Id = "X100",
                HolidayId = "1",
                Title = "Title",
                Description = "Desription",
                Amount = 100,
                Paid = 10,
            };
            Dictionary<string, object> expected = new()
            {
                {"holidayId", $"{model.HolidayId}"},
                {"title", $"{model.Title}"},
                {"description", $"{model.Description}"},
                {"amount", $"{model.Amount}"},
                {"paid", $"{model.Paid}"}
            };

            // Act:
            var actual = ExpenseConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
