using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using DAL.Interfaces;
using Google.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Services
{
    /// <summary>
    /// Сервис Гостя
    /// </summary>
    public class MemberService : BaseService, IMemberService
    {
        #region Конструкторы

        /// <summary>
        /// Конструкор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public MemberService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<bool> Create(MemberDto itemDto)
        {
            var member = new Member
            {
                Id = itemDto.Id,
                HolidayId = itemDto.HolidayId,
                MemberCategoryId = itemDto.MemberCategoryId,
                MemberStatusId = itemDto.MemberStatusId,
                MenuCategoryId = itemDto.MenuCategoryId,
                FIO = itemDto.FIO,
                PhoneNumber = itemDto.PhoneNumber,
                Email = itemDto.Email,
                Comment = itemDto.Comment,
                IsChild = itemDto.IsChild,
                IsMale = itemDto.IsMale,
                Seat = itemDto.Seat,
            };

            await _unitOfWork.Member.Create(member);
            return await SaveAsync();
        }

        public async Task<bool> Delete(string id)
        {
            if (!await _unitOfWork.Member.Exists(id))
                return false;

            await _unitOfWork.Member.Delete(id);
            return await SaveAsync();
        }

        public async Task<bool> Exists(string id) => await _unitOfWork.Member.Exists(id);

        public async Task<List<MemberDto>> GetAll()
        {
            var items = await _unitOfWork.Member.GetAll();

            var result = items
                .Select(item => new MemberDto(item))
                .ToList();

            return result;
        }

        public async Task<List<MemberDto>> GetAllByHolidayId(string holidayId)
        {
            var items = await _unitOfWork.Member.GetAllByHolidayId(holidayId);

            var result = items
                .Select(item => new MemberDto(item))
                .ToList();

            return result;
        }

        public async Task<MemberDto> GetById(string id)
        {
            var item = await _unitOfWork.Member.GetItem(id);

            return item == null ? null : new MemberDto(item);
        }

        public async Task<bool> Update(MemberDto itemDto)
        {
            if (!await _unitOfWork.Member.Exists(itemDto.Id))
                return false;

            Member item = await _unitOfWork.Member.GetItem(itemDto.Id);

            item.Id = itemDto.Id;
            item.HolidayId = itemDto.HolidayId;
            item.MemberCategoryId = itemDto.MemberCategoryId;
            item.MemberStatusId = itemDto.MemberStatusId;
            item.MenuCategoryId = itemDto.MenuCategoryId;
            item.FIO = itemDto.FIO;
            item.PhoneNumber = itemDto.PhoneNumber;
            item.Email = itemDto.Email;
            item.Comment = itemDto.Comment;
            item.IsChild = itemDto.IsChild;
            item.IsMale = itemDto.IsMale;
            item.Seat = itemDto.Seat;

            await _unitOfWork.Member.Update(item);
            return await SaveAsync();
        }

        public async Task<bool> PatchMemberStatus(PatchMemberStatusDto itemDto)
        {
            if (!await _unitOfWork.Member.Exists(itemDto.MemberId))
                return false;

            await _unitOfWork.Member.PatchMemberStatus(itemDto.MemberId, itemDto.MemberStatusId);
            return await SaveAsync();
        }

        #endregion
    }
}
