using BLL.Abstract;
using BLL.DTOs;
using BLL.Intefaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Сервис Подрядчика
    /// </summary>
    public class ContractorService : BaseService, IContractorService
    {
        #region Конструкторы

        /// <summary>
        /// Контрукор на основе UnitOfWork
        /// </summary>
        /// <param name="unitOfWork">Экземпляр UnitOfWork</param>
        public ContractorService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Методы

        public async Task<bool> Create(ContractorDto itemDto)
        {
            var contractor = new Contractor
            {
                Id = itemDto.Id,
                HolidayId = itemDto.HolidayId,
                StatusId = itemDto.StatusId,
                СategoryId = itemDto.СategoryId,
                Title = itemDto.Title,
                Description = itemDto.Description,
                PhoneNumber = itemDto.PhoneNumber,
                Email = itemDto.Email,
                ServiceCost = itemDto.ServiceCost,
            };

            await _unitOfWork.Contractor.Create(contractor);
            return await SaveAsync();
        }

        public async Task<bool> Delete(string id)
        {
            if (!await _unitOfWork.Contractor.Exists(id))
                return false;

            await _unitOfWork.Contractor.Delete(id);
            return await SaveAsync();
        }

        public async Task<bool> Exists(string id) => await _unitOfWork.Contractor.Exists(id);

        public async Task<List<ContractorDto>> GetAll()
        {
            var items = await _unitOfWork.Contractor.GetAll();

            var result = items
                .Select(item => new ContractorDto(item))
                .ToList();

            return result;
        }

        public async Task<List<ContractorDto>> GetAllByHolidayId(string holidayId)
        {
            var items = await _unitOfWork.Contractor.GetAllByHolidayId(holidayId);

            var result = items
                .Select(item => new ContractorDto(item))
                .ToList();

            return result;
        }

        public async Task<ContractorDto> GetById(string id)
        {
            var item = await _unitOfWork.Contractor.GetItem(id);

            return item == null ? null : new ContractorDto(item);
        }

        public async Task<bool> Update(ContractorDto itemDto)
        {
            if (!await _unitOfWork.Contractor.Exists(itemDto.Id))
                return false;

            Contractor item = await _unitOfWork.Contractor.GetItem(itemDto.Id);

            item.Id = itemDto.Id;
            item.HolidayId = itemDto.HolidayId;
            item.StatusId = itemDto.StatusId;
            item.СategoryId = itemDto.СategoryId;
            item.Title = itemDto.Title;
            item.Description = itemDto.Description;
            item.PhoneNumber = itemDto.PhoneNumber;
            item.Email = itemDto.Email;
            item.ServiceCost = itemDto.ServiceCost;

            await _unitOfWork.Contractor.Update(item);
            return await SaveAsync();
        }

        public async Task<bool> PatchContractorStatus(PatchContractorStatusDto itemDto)
        {
            if (!await _unitOfWork.Contractor.Exists(itemDto.ContractorId))
                return false;

            await _unitOfWork.Contractor.PatchContractorStatus(itemDto.ContractorId, itemDto.ContractorStatusId);
            return await SaveAsync();
        }

        #endregion
    }
}
