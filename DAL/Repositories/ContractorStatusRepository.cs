using DAL.Abstract;
using DAL.Converters;
using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Статуса подрядчика
    /// </summary>
    public class ContractorStatusRepository : BaseRepository, IFrozenCollectionRepository<ContractorStatus>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorStatusRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<ContractorStatus>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("statuses").Document("contractor").Collection("list");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<ContractorStatus> contractorStatuses = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var contractorStatus = ContractorStatusConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                contractorStatuses.Add(contractorStatus);
            }

            return contractorStatuses;
        }

        #endregion
    }
}
