using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Категории меню
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoryController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Категории меню
        /// </summary>
        private readonly IMenuCategoryService _menuCategoryService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Категории меню
        /// </summary>
        /// <param name="menuCategoryService">Сервис Категории меню</param>
        public MenuCategoryController(IMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MenuCategoryDto>>> GetAll()
        {
            var categories = (await _menuCategoryService.GetAll()).ToList();

            return Ok(categories);
        }

        #endregion
    }
}
