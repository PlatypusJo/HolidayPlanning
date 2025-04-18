using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Задачи
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Задачи
        /// </summary>
        private readonly IGoalService _goalService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Задачи
        /// </summary>
        /// <param name="goalService">Сервис Задачи</param>
        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetById(string id)
        {
            if (!await _goalService.Exists(id))
            {
                return NotFound("Invalid id, entity not exists.");
            }

            return await _goalService.GetById(id);
        }

        /// <summary>
        /// Возвращает всех задачаов выбранного мероприятия
        /// </summary>
        /// <param name="id">ID мероприятия</param>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("HolidayId/{id}")]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetAllByHolidayId(string id)
        {
            var goals = (await _goalService.GetAllByHolidayId(id)).ToList();

            return Ok(goals);
        }

        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="goalDto">DTO сущности</param>
        /// <returns>Созданная сущность (В виде OkObjectResult)</returns>
        [HttpPost]
        public async Task<ActionResult<GoalDto>> Create([FromBody] GoalDto goalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _goalService.Create(goalDto))
            {
                return NotFound();
            }

            var createdItem = await _goalService.GetById((await _goalService.GetAll()).Max(x => x.Id));
            return Ok(createdItem);
        }

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="id">ID заданной сущности</param>
        /// <param name="goalDto">Измененная сущность в форме dto</param>
        /// <returns>ID обновленной сущности (В виде OkObjectResult)</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(string id, [FromBody] GoalDto goalDto)
        {
            if (id != goalDto.Id)
            {
                return BadRequest();
            }

            if (!await _goalService.Update(goalDto))
            {
                return NotFound();
            }

            return Ok(goalDto.Id);
        }

        /// <summary>
        /// Обновляет статус задачи
        /// </summary>
        /// <param name="id">ID Задачи</param>
        /// <param name="patchGoalStatusDto">Dto с измененным статусом задачи</param>
        /// <returns></returns>
        [HttpPatch("Status/GoalId/{id}")]
        public async Task<ActionResult<int>> Patch(string id, [FromBody] PatchGoalStatusDto patchGoalStatusDto)
        {
            if (id != patchGoalStatusDto.GoalId)
            {
                return BadRequest();
            }

            if (!await _goalService.PatchGoalStatus(patchGoalStatusDto))
            {
                return NotFound();
            }

            return Ok(patchGoalStatusDto.GoalId);
        }

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> ID удаленной сущности (В виде OkObjectResult) </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            if (!await _goalService.Exists(id))
            {
                return NotFound();
            }

            await _goalService.Delete(id);
            return Ok(id);
        }

        #endregion
    }
}
