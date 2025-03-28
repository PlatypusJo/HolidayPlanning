using DAL.Abstract;
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
    /// Репозиторий Статуса гостя
    /// </summary>
    public class MemberStatusRepository : BaseRepository, IFrozenCollectionRepository<MemberStatus>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public MemberStatusRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<MemberStatus>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("statuses").Document("member").Collection("list");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<MemberStatus> memberStatuses = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                memberStatuses.Add(new MemberStatus()
                {
                    Id = document.Id,
                    Title = documentTemp["text"].ToString(),
                });
            }

            return memberStatuses;
        }

        #endregion
    }
}
