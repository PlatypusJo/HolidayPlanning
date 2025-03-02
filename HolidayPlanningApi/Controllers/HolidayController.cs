using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Мероприятия
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса мероприятия
        /// </summary>
        private readonly IHolidayService _holidayService;
        
        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса мероприятия
        /// </summary>
        /// <param name="holidayService">Сервис мероприятия</param>
        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        
        #endregion

        #region Методы

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        [HttpGet("{id}")]
        public Task<HolidayDto> GetById(int id)
        {
            return _holidayService.GetById(id);
        }

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HolidayDto>>> GetAll()
        {
            var holidays = (await _holidayService.GetAll()).ToList();

            return Ok(holidays);
        }

        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="holidayDto">DTO сущности</param>
        /// <returns>Созданная сущность (В виде OkObjectResult)</returns>
        [HttpPost]
        public async Task<ActionResult<HolidayDto>> Create([FromBody] HolidayDto holidayDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _holidayService.Create(holidayDto))
            {
                return NotFound();
            }

            var createdItem = await _holidayService.GetById((await _holidayService.GetAll()).Max(x => x.Id));
            return Ok(createdItem);
        }

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="id">ID заданной сущности</param>
        /// <param name="holidayDto">Измененная сущность в форме dto</param>
        /// <returns>ID обновленной сущности (В виде OkObjectResult)</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(int id, [FromBody] HolidayDto holidayDto)
        {
            if (id != holidayDto.Id)
            {
                return BadRequest();
            }

            if (!await _holidayService.Update(holidayDto))
            {
                return NotFound();
            }

            return Ok(holidayDto.Id);
        }

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> ID удаленной сущности (В виде OkObjectResult) </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            if (!await _holidayService.Exists(id))
            {
                return NotFound();
            }

            await _holidayService.Delete(id);
            return Ok(id);
        }

        #endregion
    }
}
