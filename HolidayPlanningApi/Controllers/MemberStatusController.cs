using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Статуса гостя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MemberStatusController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Статуса гостя
        /// </summary>
        private readonly IMemberStatusService _memberStatusService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Статуса гостя
        /// </summary>
        /// <param name="memberStatusService">Сервис Статуса гостя</param>
        public MemberStatusController(IMemberStatusService memberStatusService)
        {
            _memberStatusService = memberStatusService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает все экземпляры сущности в виде dto
        /// </summary>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MemberStatusDto>>> GetAll()
        {
            var categories = (await _memberStatusService.GetAll()).ToList();

            return Ok(categories);
        }

        #endregion
    }
}
