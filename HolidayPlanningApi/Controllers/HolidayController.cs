using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HolidayPlanningApi.Controllers
{
    /// <summary>
    /// �����-���������� �����������
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        #region ����

        /// <summary>
        /// ��������� ������� �����������
        /// </summary>
        private readonly IHolidayService _holidayService;
        
        #endregion

        #region ������������

        /// <summary>
        /// ���������� �� ������ ������� �����������
        /// </summary>
        /// <param name="holidayService">������ �����������</param>
        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        
        #endregion

        #region ������

        /// <summary>
        /// ���������� dto �������� �� ��������� ID
        /// </summary>
        /// <param name="id">ID ��������</param>
        /// <returns>��������� �������� � ����� dto (null, ���� �������� �� �������)</returns>
        [HttpGet("{id}")]
        public Task<HolidayDto> GetById(int id)
        {
            return _holidayService.GetById(id);
        }

        /// <summary>
        /// ���������� ��� ���������� �������� � ���� dto
        /// </summary>
        /// <returns>������ dto ��������� (� ���� OkObjectResult)</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HolidayDto>>> GetAll()
        {
            var holidays = (await _holidayService.GetAll()).ToList();

            return Ok(holidays);
        }

        /// <summary>
        /// ������� �������� �� ������ ��������� DTO
        /// </summary>
        /// <param name="holidayDto">DTO ��������</param>
        /// <returns>��������� �������� (� ���� OkObjectResult)</returns>
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
        /// ��������� �������� ��������
        /// </summary>
        /// <param name="id">ID �������� ��������</param>
        /// <param name="holidayDto">���������� �������� � ����� dto</param>
        /// <returns>ID ����������� �������� (� ���� OkObjectResult)</returns>
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
        /// ������� �������� �� ��������� ID
        /// </summary>
        /// <param name="id">ID ��������</param>
        /// <returns> ID ��������� �������� (� ���� OkObjectResult) </returns>
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
