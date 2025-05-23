﻿using DAL.Abstract;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Converters;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Категории подрядчика
    /// </summary>
    public class ContractorCategoryRepository : BaseRepository, IFrozenCollectionRepository<ContractorCategory>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public ContractorCategoryRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task<List<ContractorCategory>> GetAll()
        {
            CollectionReference categoryRef = _db.Collection("categories").Document("contractor").Collection("list");
            QuerySnapshot snapshot = await categoryRef.GetSnapshotAsync();

            List<ContractorCategory> categories = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var category = ContractorCategoryConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                categories.Add(category);
            }

            return categories;
        }

        #endregion
    }
}
