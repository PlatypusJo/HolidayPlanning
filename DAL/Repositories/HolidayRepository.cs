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
    /// Репозиторий Мероприятия
    /// </summary>
    public class HolidayRepository : BaseRepository, IRepository<Holiday>
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public HolidayRepository(FirestoreDb db) : base(db) { }
        
        #endregion

        #region Методы

        public async Task Create(Holiday item)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{item.Id}");
            Dictionary<string, object> holiday = new Dictionary<string, object>
            {
                {"Title", $"{item.Title}"},
                {"StartDate", item.StartDate.ToString()},
                {"EndDate", item.EndDate.ToString()},
                {"Budget", item.Budget.ToString()}

            };
            await docRef.SetAsync(holiday);
        }

        public async Task<bool> Delete(string id)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{id}");
            return await docRef.DeleteAsync() is not null;
        }

        public async Task<bool> Exists(string id)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{id}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task<List<Holiday>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("contractors");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<Holiday> holidays = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                holidays.Add(new Holiday()
                {
                    Id = document.Id,
                    Title = documentTemp["Title"].ToString(),
                    Budget = Convert.ToDouble(documentTemp["Budget"].ToString()),
                    StartDate = DateTime.Parse(documentTemp["StartDate"].ToString()),
                    EndDate = DateTime.Parse(documentTemp["EndDate"].ToString())
                });
            }

            return holidays;
        }

        public async Task<Holiday> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            var documentTemp = document.ToDictionary();

            Holiday holiday = new Holiday()
            {
                Id = document.Id,
                Title = documentTemp["Title"].ToString(),
                Budget = Convert.ToDouble(documentTemp["Budget"].ToString()),
                StartDate = DateTime.Parse(documentTemp["StartDate"].ToString()),
                EndDate = DateTime.Parse(documentTemp["EndDate"].ToString())
            };

            return holiday;
        }

        public async Task Update(Holiday item)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{item.Id}");
            Dictionary<string, object> holiday = new Dictionary<string, object>
            {
                {"Title", $"{item.Title}"},
                {"StartDate", item.StartDate.ToString()},
                {"EndDate", item.EndDate.ToString()},
                {"Budget", item.Budget.ToString()}
            };

            await docRef.UpdateAsync(holiday);
        }
        
        #endregion
    }
}
