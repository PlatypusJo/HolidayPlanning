using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Статуса подрядчика
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorStatusController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Статуса подрядчика
        /// </summary>
        private readonly IContractorStatusService _contractorStatusService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Статуса подрядчика
        /// </summary>
        /// <param name="contractorStatusService">Сервис Статуса подрядчика</param>
        public ContractorStatusController(IContractorStatusService contractorStatusService)
        {
            _contractorStatusService = contractorStatusService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ContractorStatusDto>>> GetAll()
        {
            var statuses = (await _contractorStatusService.GetAll()).ToList();

            return Ok(statuses);
        }

        #endregion
    }
}
