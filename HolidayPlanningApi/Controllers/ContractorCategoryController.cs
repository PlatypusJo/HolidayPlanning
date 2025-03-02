using BLL.DTOs;
using BLL.Intefaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Категории подрядчика
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorCategoryController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Категории подрядчика
        /// </summary>
        private readonly IContractorCategoryService _contractorCategoryService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Категории подрядчика
        /// </summary>
        /// <param name="contractorCategoryService">Сервис Категории подрядчика</param>
        public ContractorCategoryController(IContractorCategoryService contractorCategoryService)
        {
            _contractorCategoryService = contractorCategoryService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ContractorCategoryDto>>> GetAll()
        {
            var categories = (await _contractorCategoryService.GetAll()).ToList();

            return Ok(categories);
        }

        #endregion
    }
}
