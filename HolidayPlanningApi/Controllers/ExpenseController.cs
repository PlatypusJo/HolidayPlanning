using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Статьи расходов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Статьи расходов
        /// </summary>
        private readonly IExpenseService _expenseService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Статьи расходов
        /// </summary>
        /// <param name="expenseSevice">Сервис Статьи расходов</param>
        public ExpenseController(IExpenseService expenseSevice)
        {
            _expenseService = expenseSevice;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetById(string id)
        {
            if (!await _expenseService.Exists(id))
            {
                return NotFound("Invalid id, entity not exists.");
            }

            return await _expenseService.GetById(id);
        }

        /// <summary>
        /// Возвращает все статьи расходов выбранного мероприятия
        /// </summary>
        /// <param name="id">ID мероприятия</param>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("HolidayId/{id}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAllByHolidayId(string id)
        {
            var expenses = (await _expenseService.GetAllByHolidayId(id)).ToList();

            return Ok(expenses);
        }

        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="expenseDto">DTO сущности</param>
        /// <returns>Созданная сущность (В виде OkObjectResult)</returns>
        [HttpPost]
        public async Task<ActionResult<ContractorDto>> Create([FromBody] ExpenseDto expenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _expenseService.Create(expenseDto))
            {
                return NotFound();
            }

            var createdItem = await _expenseService.GetById((await _expenseService.GetAll()).Max(x => x.Id));
            return Ok(createdItem);
        }

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="id">ID заданной сущности</param>
        /// <param name="expenseDto">Измененная сущность в форме dto</param>
        /// <returns>ID обновленной сущности (В виде OkObjectResult)</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(string id, [FromBody] ExpenseDto expenseDto)
        {
            if (id != expenseDto.Id)
            {
                return BadRequest();
            }

            if (!await _expenseService.Update(expenseDto))
            {
                return NotFound();
            }

            return Ok(expenseDto.Id);
        }

        /// <summary>
        /// Обновляет оплаченную сумму Статьи расходов
        /// </summary>
        /// <param name="id">ID Статьи расходов</param>
        /// <param name="patchExpensePaidDto">Dto с измененной оплаченной суммой</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Patch(string id, [FromBody] PatchExpensePaidDto patchExpensePaidDto)
        {
            if (id != patchExpensePaidDto.ExpenseId)
            {
                return BadRequest();
            }

            if (!await _expenseService.PatchPaid(patchExpensePaidDto))
            {
                return NotFound();
            }

            return Ok(patchExpensePaidDto.ExpenseId);
        }

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> ID удаленной сущности (В виде OkObjectResult) </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            if (!await _expenseService.Exists(id))
            {
                return NotFound();
            }

            await _expenseService.Delete(id);
            return Ok(id);
        }

        #endregion
    }
}
