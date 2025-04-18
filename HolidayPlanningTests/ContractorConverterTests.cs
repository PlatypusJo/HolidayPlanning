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
    /// Тесты для конвертора Подрядчика
    /// </summary>
    public class ContractorConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"title", "Title"},
                {"description", "Description"},
                {"email", "test@test.ru"},
                {"phoneNumber", "+11111111111"},
                {"serviceCost", 100},
                {"paid", 10},
                {"categoryId", "1"},
                {"statusId", "1"},
                {"holidayId", "1"}
            };
            Contractor expected = new Contractor()
            {
                Id = "X100",
                Title = "Title",
                ContractorСategoryId = "1",
                ContractorStatusId = "1",
                HolidayId = "1",
                Description = "Description",
                Email = "test@test.ru",
                PhoneNumber = "+11111111111",
                ServiceCost = 100,
                Paid = 10,
            };

            // Act:
            var actual = ContractorConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            Contractor model = new()
            {
                Id = "X100",
                Title = "Title",
                ContractorСategoryId = "1",
                ContractorStatusId = "1",
                HolidayId = "1",
                Description = "Description",
                Email = "test@test.ru",
                PhoneNumber = "+11111111111",
                ServiceCost = 100,
                Paid = 10,
            };
            Dictionary<string, object> expected = new()
            {
                {"title", $"{model.Title}"},
                {"description", $"{model.Description}"},
                {"email", $"{model.Email}"},
                {"phoneNumber", $"{model.PhoneNumber}"},
                {"serviceCost", $"{model.ServiceCost}"},
                {"paid", $"{model.Paid}"},
                {"categoryId", $"{model.ContractorСategoryId}"},
                {"statusId", $"{model.ContractorStatusId}"},
                {"holidayId", $"{model.HolidayId}"}
            };

            // Act:
            var actual = ContractorConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
