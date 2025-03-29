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
    /// Репозиторий Категории меню
    /// </summary>
    public class MenuCategoryRepository : BaseRepository, IFrozenCollectionRepository<MenuCategory>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public MenuCategoryRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<MenuCategory>> GetAll()
        {
            CollectionReference categoryRef = _db.Collection("categories").Document("menu").Collection("list");
            QuerySnapshot snapshot = await categoryRef.GetSnapshotAsync();

            List<MenuCategory> categories = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                categories.Add(new MenuCategory()
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
