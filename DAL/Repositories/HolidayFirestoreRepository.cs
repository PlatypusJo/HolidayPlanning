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
    public class HolidayFirestoreRepository : FireStoreBaseRepository, IHolidayRepositoryFirestore<Holiday>
    {
        #region Конструктор

        public HolidayFirestoreRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Holiday item)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{item.Id}");
            Dictionary<string, object> holiday = new Dictionary<string, object>
            {
                {"Title", $"{item.Title}"},
                {"StartDate", $"{item.StartDate}"},
                {"EndDate", $"{item.EndDate}"},
                {"Budget", $"{item.Budget}"}
            };
            await docRef.SetAsync(holiday);
        }

        public async Task<WriteResult> Delete(int id)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{id}");
            return await docRef.DeleteAsync();
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
                    Id = Convert.ToInt32(document.Id),
                    Title = documentTemp["Title"].ToString(),
                    StartDate = DateTime.Parse(documentTemp["StartDate"].ToString()),
                    EndDate = DateTime.Parse(documentTemp["EndDate"].ToString()),
                    Budget = Convert.ToDouble(documentTemp["Budget"].ToString()),
                });
            }

            return holidays;
        }

        public async Task<WriteResult> Update(Holiday item)
        {
            DocumentReference docRef = _db.Collection("holiday").Document($"{item.Id}");
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                {"Title", $"{item.Title}"},
                {"StartDate", $"{item.StartDate}"},
                {"EndDate", $"{item.EndDate}"},
                {"Budget", $"{item.Budget}"}
            };
            return await docRef.UpdateAsync(updates);
        }

        #endregion
        
    }
}
