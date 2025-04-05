using BLL.DTOs;
using BLL.Intefaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Подрядчика
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Подрядчика
        /// </summary>
        private readonly IContractorService _contractorService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Подрядчика
        /// </summary>
        /// <param name="contractorService">Сервис Подрядчика</param>
        public ContractorController(IContractorService contractorService)
        {
            _contractorService = contractorService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractorDto>> GetById(string id)
        {
            if (!await _contractorService.Exists(id))
            {
                return NotFound("Invalid id, entity not exists.");
            }

            return await _contractorService.GetById(id);
        }

        /// <summary>
        /// Возвращает всех подрядчиков выбранного мероприятия
        /// </summary>
        /// <param name="id">ID мероприятия</param>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("HolidayId/{id}")]
        public async Task<ActionResult<IEnumerable<ContractorDto>>> GetAllByHolidayId(string id)
        {
            var contractors = (await _contractorService.GetAllByHolidayId(id)).ToList();

            return Ok(contractors);
        }

        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="contractorDto">DTO сущности</param>
        /// <returns>Созданная сущность (В виде OkObjectResult)</returns>
        [HttpPost]
        public async Task<ActionResult<ContractorDto>> Create([FromBody] ContractorDto contractorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _contractorService.Create(contractorDto))
            {
                return NotFound();
            }

            var createdItem = await _contractorService.GetById((await _contractorService.GetAll()).Max(x => x.Id));
            return Ok(createdItem);
        }

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="id">ID заданной сущности</param>
        /// <param name="contractorDto">Измененная сущность в форме dto</param>
        /// <returns>ID обновленной сущности (В виде OkObjectResult)</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(string id, [FromBody] ContractorDto contractorDto)
        {
            if (id != contractorDto.Id)
            {
                return BadRequest();
            }

            if (!await _contractorService.Update(contractorDto))
            {
                return NotFound();
            }

            return Ok(contractorDto.Id);
        }

        /// <summary>
        /// Обновляет статус подрядчика
        /// </summary>
        /// <param name="id">ID Подрядчика</param>
        /// <param name="patchContractorStatusDto">Dto с измененным статусом подрядчика</param>
        /// <returns></returns>
        [HttpPatch("Status/ContractorId/{id}")]
        public async Task<ActionResult<int>> Patch(string id, [FromBody] PatchContractorStatusDto patchContractorStatusDto)
        {
            if (id != patchContractorStatusDto.ContractorId)
            {
                return BadRequest();
            }

            if (!await _contractorService.PatchContractorStatus(patchContractorStatusDto))
            {
                return NotFound();
            }

            return Ok(patchContractorStatusDto.ContractorId);
        }

        /// <summary>
        /// Обновляет оплаченную сумму услуги Подрядчика
        /// </summary>
        /// <param name="id">ID Подрядчика</param>
        /// <param name="patchContractorPaidDto">Dto с измененной оплаченной суммой</param>
        /// <returns></returns>
        [HttpPatch("Paid/ContractorId/{id}")]
        public async Task<ActionResult<int>> Patch(string id, [FromBody] PatchContractorPaidDto patchContractorPaidDto)
        {
            if (id != patchContractorPaidDto.ContractorId)
            {
                return BadRequest();
            }

            if (!await _contractorService.PatchPaid(patchContractorPaidDto))
            {
                return NotFound();
            }

            return Ok(patchContractorPaidDto.ContractorId);
        }

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> ID удаленной сущности (В виде OkObjectResult) </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            if (!await _contractorService.Exists(id))
            {
                return NotFound();
            }

            await _contractorService.Delete(id);
            return Ok(id);
        }

        #endregion
    }
}
