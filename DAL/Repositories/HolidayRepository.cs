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
    public class HolidayRepository : BaseRepository, IHolidayRepository
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
                {"title", $"{item.Title}"},
                {"startDate", item.StartDate.ToString()},
                {"userId", item.UserId},
                {"endDate", item.EndDate.ToString()},
                {"budget", item.Budget.ToString()}

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
            CollectionReference usersRef = _db.Collection("holiday");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<Holiday> holidays = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                holidays.Add(new Holiday()
                {
                    Id = document.Id,
                    Title = documentTemp["title"].ToString(),
                    UserId = documentTemp["userId"].ToString(),
                    Budget = Convert.ToDouble(documentTemp["budget"].ToString()),
                    StartDate = DateTime.Parse(documentTemp["startDate"].ToString()),
                    EndDate = DateTime.Parse(documentTemp["endDate"].ToString())
                });
            }

            return holidays;
        }

        public async Task<List<Holiday>> GetAllByUserId(string userId)
        {
            CollectionReference docRef = _db.Collection("holiday");
            Query query = docRef.WhereEqualTo("userId", userId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Holiday> holidays = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var documentTemp = document.ToDictionary();
                holidays.Add(new Holiday()
                {
                    Id = document.Id,
                    Title = documentTemp["title"].ToString(),
                    UserId = documentTemp["userId"].ToString(),
                    Budget = Convert.ToDouble(documentTemp["budget"].ToString()),
                    StartDate = DateTime.Parse(documentTemp["startDate"].ToString()),
                    EndDate = DateTime.Parse(documentTemp["endDate"].ToString())
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
                Title = documentTemp["title"].ToString(),
                UserId = documentTemp["userId"].ToString(),
                Budget = Convert.ToDouble(documentTemp["budget"].ToString()),
                StartDate = DateTime.Parse(documentTemp["startDate"].ToString()),
                EndDate = DateTime.Parse(documentTemp["endDate"].ToString())
            };

            return holiday;
        }

        public async Task Update(Holiday item)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{item.Id}");
            Dictionary<string, object> holiday = new Dictionary<string, object>
            {
                {"title", $"{item.Title}"},
                {"startDate", item.StartDate.ToString()},
                {"userId", item.UserId},
                {"endDate", item.EndDate.ToString()},
                {"budget", item.Budget.ToString()}
            };

            await docRef.UpdateAsync(holiday);
        }
        
        #endregion
    }
}
