using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Подрядчика
    /// </summary>
    public class ContractorRepository : BaseRepository, IContractorRepository
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Contractor item)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{item.Id}");
            Dictionary<string, object> contractor = new Dictionary<string, object>
            {
                {"title", $"{item.Title}"},
                {"description", $"{item.Description}"},
                {"email", $"{item.Email}"},
                {"phoneNumber", $"{item.PhoneNumber}"},
                {"serviceCost", $"{item.ServiceCost}"},
                {"categoryId", $"{item.СategoryId}"},
                {"statusId", $"{item.StatusId}"},
                {"holidayId", $"{item.HolidayId}"}

            };
            await docRef.SetAsync(contractor);
        }
        
        public async Task<bool> Delete(string id)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{id}");
            return await docRef.DeleteAsync() is not null;
        }

        public async Task<bool> Exists(string id) 
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{id}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task<List<Contractor>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("contractors");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<Contractor> contractors = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                contractors.Add(new Contractor()
                {
                    Id = document.Id,
                    Title = documentTemp["title"].ToString(),
                    СategoryId = documentTemp["categoryId"].ToString(),
                    StatusId = documentTemp["statusId"].ToString(),
                    HolidayId = documentTemp["holidayId"].ToString(),
                    Description = documentTemp["description"].ToString(),
                    Email = documentTemp["email"].ToString(),
                    PhoneNumber = documentTemp["phoneNumber"].ToString(),
                    ServiceCost = Convert.ToDouble(documentTemp["serviceCost"].ToString()),
                });
            }

            return contractors;
        }

        public async Task<List<Contractor>> GetAllByHolidayId(string holidayId)
        {
            CollectionReference docRef = _db.Collection("contractors");
            Query query = docRef.WhereEqualTo("holidayId", holidayId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Contractor> contractors = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                contractors.Add(new Contractor()
                {
                    Id = document.Id,
                    Title = documentTemp["title"].ToString(),
                    СategoryId = documentTemp["categoryId"].ToString(),
                    StatusId = documentTemp["statusId"].ToString(),
                    HolidayId = documentTemp["holidayId"].ToString(),
                    Description = documentTemp["description"].ToString(),
                    Email = documentTemp["email"].ToString(),
                    PhoneNumber = documentTemp["phoneNumber"].ToString(),
                    ServiceCost = Convert.ToDouble(documentTemp["serviceCost"].ToString()),
                });
            }

            return contractors;
        }

        public async Task<Contractor> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            var documentTemp = document.ToDictionary();

            Contractor contractor = new Contractor()
            {
                Id = document.Id,
                Title = documentTemp["title"].ToString(),
                СategoryId = documentTemp["categoryId"].ToString(),
                StatusId = documentTemp["statusId"].ToString(),
                HolidayId = documentTemp["holidayId"].ToString(),
                Description = documentTemp["description"].ToString(),
                Email = documentTemp["email"].ToString(),
                PhoneNumber = documentTemp["phoneNumber"].ToString(),
                ServiceCost = Convert.ToDouble(documentTemp["serviceCost"].ToString()),
            };

            return contractor;
        }

        public async Task Update(Contractor item)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{item.Id}");
            Dictionary<string, object> contractor = new Dictionary<string, object>
            {
                {"title", $"{item.Title}"},
                {"description", $"{item.Description}"},
                {"email", $"{item.Email}"},
                {"phoneNumber", $"{item.PhoneNumber}"},
                {"serviceCost", $"{item.ServiceCost}"},
                {"categoryId", $"{item.СategoryId}"},
                {"statusId", $"{item.StatusId}"},
                {"holidayId", $"{item.HolidayId}"}
            };

            await docRef.UpdateAsync(contractor);
        }

        #endregion
    }
}
