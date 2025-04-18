using DAL.Abstract;
using DAL.Converters;
using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Статуса задачи
    /// </summary>
    public class GoalStatusRepository : BaseRepository, IFrozenCollectionRepository<GoalStatus>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public GoalStatusRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<GoalStatus>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("statuses").Document("goal").Collection("list");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<GoalStatus> goalStatuses = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var goalStatus = GoalStatusConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                goalStatuses.Add(goalStatus);
            }

            return goalStatuses;
        }

        #endregion
    }
}
