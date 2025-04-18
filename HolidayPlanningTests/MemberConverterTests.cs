using DAL.Converters;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanningTests
{
    public class MemberConverterTests
    {
        [Fact]
        public void FromDictionaryToModal_PassDictionary_GetCorrectEntityModel_Test()
        {
            // Arrange:
            Dictionary<string, object> dictionary = new()
            {
                {"holidayId", "1"},
                {"memberCategoryId", "1"},
                {"memberStatusId", "1"},
                {"menuCategoryId", "1"},
                {"fio", "TestFio"},
                {"phoneNumber", "+11111111111"},
                {"email", "test@test.ru"},
                {"comment", "Comment"},
                {"isChild", "false"},
                {"isMale", "true"},
                {"seat", "Middle"},
            };
            Member expected = new()
            {
                Id = "X100",
                HolidayId = "1",
                MemberCategoryId = "1",
                MemberStatusId = "1",
                MenuCategoryId = "1",
                FIO = "TestFio",
                PhoneNumber = "+11111111111",
                Email = "test@test.ru",
                Comment = "Comment",
                IsChild = false,
                IsMale = true,
                Seat = "Middle"
            };

            // Act:
            var actual = MemberConverter.FromDictionaryToModel(dictionary, "X100");

            // Assert:
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public void FromModelToDictionary_PassModel_GetCorrectDictionary_Test()
        {
            // Arrange:
            Member model = new()
            {
                Id = "X100",
                HolidayId = "1",
                MemberCategoryId = "1",
                MemberStatusId = "1",
                MenuCategoryId = "1",
                FIO = "TestFio",
                PhoneNumber = "+11111111111",
                Email = "test@test.ru",
                Comment = "Comment",
                IsChild = false,
                IsMale = true,
                Seat = "Middle"
            };
            Dictionary<string, object> expected = new()
            {
                {"holidayId", $"{model.HolidayId}"},
                {"memberCategoryId", $"{model.MemberCategoryId}"},
                {"memberStatusId", $"{model.MemberStatusId}"},
                {"menuCategoryId", $"{model.MenuCategoryId}"},
                {"fio", $"{model.FIO}"},
                {"phoneNumber", $"{model.PhoneNumber}"},
                {"email", $"{model.Email}"},
                {"comment", $"{model.Comment}"},
                {"isChild", $"{model.IsChild}"},
                {"isMale", $"{model.IsMale}"},
                {"seat", $"{model.Seat}"},
            };

            // Act:
            var actual = MemberConverter.FromModelToDictionary(model);

            // Assert:
            Assert.Equivalent(expected, actual);
        }
    }
}
