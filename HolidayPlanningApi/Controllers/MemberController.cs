using BLL.DTOs;
using BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// Класс-контроллер Гостя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        #region Поля

        /// <summary>
        /// Экземпляр сервиса Гостя
        /// </summary>
        private readonly IMemberService _memberService;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Контруктор на основе сервиса Гостя
        /// </summary>
        /// <param name="memberService">Сервис Гостя</param>
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возвращает dto сущности по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>Найденная сущность в форме dto (null, если сущность не найдена)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetById(string id)
        {
            if(!await _memberService.Exists(id))
            {
                return NotFound("Invalid id, entity not exists.");
            }

            return await _memberService.GetById(id);
        }

        /// <summary>
        /// Возвращает всех гостей выбранного мероприятия
        /// </summary>
        /// <param name="id">ID мероприятия</param>
        /// <returns>Список dto сущностей (В виде OkObjectResult)</returns>
        [HttpGet("HolidayId/{id}")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllByHolidayId(string id)
        {
            var members = (await _memberService.GetAllByHolidayId(id)).ToList();

            return Ok(members);
        }

        /// <summary>
        /// Создает сущность на основе заданного DTO
        /// </summary>
        /// <param name="memberDto">DTO сущности</param>
        /// <returns>Созданная сущность (В виде OkObjectResult)</returns>
        [HttpPost]
        public async Task<ActionResult<MemberDto>> Create([FromBody] MemberDto memberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _memberService.Create(memberDto))
            {
                return NotFound();
            }

            var createdItem = await _memberService.GetById((await _memberService.GetAll()).Max(x => x.Id));
            return Ok(createdItem);
        }

        /// <summary>
        /// Обновляет заданную сущность
        /// </summary>
        /// <param name="id">ID заданной сущности</param>
        /// <param name="memberDto">Измененная сущность в форме dto</param>
        /// <returns>ID обновленной сущности (В виде OkObjectResult)</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(string id, [FromBody] MemberDto memberDto)
        {
            if (id != memberDto.Id)
            {
                return BadRequest();
            }

            if (!await _memberService.Update(memberDto))
            {
                return NotFound();
            }

            return Ok(memberDto.Id);
        }

        /// <summary>
        /// Обновляет статус гостя
        /// </summary>
        /// <param name="id">ID Гостя</param>
        /// <param name="patchMemberStatusDto">Dto с измененным статусом гостя</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Patch(string id, [FromBody] PatchMemberStatusDto patchMemberStatusDto)
        {
            if (id != patchMemberStatusDto.MemberId)
            {
                return BadRequest();
            }

            if (!await _memberService.PatchMemberStatus(patchMemberStatusDto))
            {
                return NotFound();
            }

            return Ok(patchMemberStatusDto.MemberId);
        }

        /// <summary>
        /// Удаляет сущность по заданному ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns> ID удаленной сущности (В виде OkObjectResult) </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
            if (!await _memberService.Exists(id))
            {
                return NotFound();
            }

            await _memberService.Delete(id);
            return Ok(id);
        }

        #endregion

    }
}
