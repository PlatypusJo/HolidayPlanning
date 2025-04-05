using BLL.DTOs;
using BLL.Intefaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер расходов мероприятия
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayExpenseController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса расходов мероприятия
        /// </summary>
        private readonly IHolidayExpenseService _holidayExpenseService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса расходов мероприятия
        /// </summary>
        /// <param name="holidayExpenseService">Сервис расходов мероприятия</param>
        public HolidayExpenseController(IHolidayExpenseService holidayExpenseService)
        {
            _holidayExpenseService = holidayExpenseService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все расходы выбранного мероприятия
        /// </summary>
        /// <param name="id">ID мероприятия</param>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("HolidayId/{id}")]
        public async Task<ActionResult<IEnumerable<HolidayExpenseDto>>> GetAll(string id)
        {
            var holidayExpense = (await _holidayExpenseService.GetAll(id)).ToList();

            return Ok(holidayExpense);
        }

        #endregion
    }
}
