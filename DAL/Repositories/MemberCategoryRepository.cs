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
    /// Репозиторий Категории гостя
    /// </summary>
    public class MemberCategoryRepository : BaseRepository, IFrozenCollectionRepository<MemberCategory>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public MemberCategoryRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<MemberCategory>> GetAll()
        {
            CollectionReference categoryRef = _db.Collection("categories").Document("member").Collection("list");
            QuerySnapshot snapshot = await categoryRef.GetSnapshotAsync();

            List<MemberCategory> categories = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                categories.Add(new MemberCategory()
                {
                    Id = document.Id,
                    Title = documentTemp["text"].ToString(),
                });
            }

            return categories;
        }

        #endregion
    }
}
