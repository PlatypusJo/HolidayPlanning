using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Категории гостя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MemberCategoryController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Категории гостя
        /// </summary>
        private readonly IMemberCategoryService _memberCategoryService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Категории гостя
        /// </summary>
        /// <param name="memberCategoryService">Сервис Категории гостя</param>
        public MemberCategoryController(IMemberCategoryService memberCategoryService)
        {
            _memberCategoryService = memberCategoryService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MemberCategoryDto>>> GetAll()
        {
            var categories = (await _memberCategoryService.GetAll()).ToList();

            return Ok(categories);
        }

        #endregion
    }
}
