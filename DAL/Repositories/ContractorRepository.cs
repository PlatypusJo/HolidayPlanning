using DAL.Abstract;
using DAL.Converters;
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
            Dictionary<string, object> contractor = ContractorConverter.FromModelToDictionary(item);
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
            CollectionReference contractorsRef = _db.Collection("contractors");
            QuerySnapshot snapshot = await contractorsRef.GetSnapshotAsync();

            List<Contractor> contractors = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Contractor contractor = ContractorConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                contractors.Add(contractor);
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
                Contractor contractor = ContractorConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                contractors.Add(contractor);
            }

            return contractors;
        }

        public async Task<Contractor> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            Contractor contractor = ContractorConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
            return contractor;
        }

        public async Task Update(Contractor item)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{item.Id}");
            Dictionary<string, object> contractor = ContractorConverter.FromModelToDictionary(item);
            await docRef.UpdateAsync(contractor);
        }

        public async Task PatchContractorStatus(string contractorId, string contractorStatusId)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{contractorId}");
            Dictionary<string, object> contractor = new Dictionary<string, object>
            {
                {"statusId", $"{contractorStatusId}"}
            };

            await docRef.UpdateAsync(contractor);
        }

        public async Task PatchPaid(string contractorId, double paid)
        {
            DocumentReference docRef = _db.Collection("contractors").Document($"{contractorId}");
            Dictionary<string, object> contractor = new Dictionary<string, object>
            {
                {"paid", $"{paid}"}
            };

            await docRef.UpdateAsync(contractor);
        }

        #endregion
    }
}
