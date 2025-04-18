using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Статуса задачи
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoalStatusController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Статуса задачи
        /// </summary>
        private readonly IGoalStatusService _goalStatusService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Статуса задачи
        /// </summary>
        /// <param name="goalStatusService">Сервис Статуса задачи</param>
        public GoalStatusController(IGoalStatusService goalStatusService)
        {
            _goalStatusService = goalStatusService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<GoalStatusDto>>> GetAll()
        {
            var categories = (await _goalStatusService.GetAll()).ToList();

            return Ok(categories);
        }

        #endregion
    }
}
