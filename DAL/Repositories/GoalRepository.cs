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
    /// Репозиторий Задачи
    /// </summary>
    public class GoalRepository : BaseRepository, IGoalRepository
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public GoalRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Goal item)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{item.Id}");
            Dictionary<string, object> goal = GoalConverter.FromModelToDictionary(item);
            await docRef.SetAsync(goal);
        }

        public async Task<bool> Delete(string id)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{id}");
            return await docRef.DeleteAsync() is not null;
        }

        public async Task<bool> Exists(string id)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{id}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task<List<Goal>> GetAll()
        {
            CollectionReference goalsRef = _db.Collection("goals");
            QuerySnapshot snapshot = await goalsRef.GetSnapshotAsync();

            List<Goal> goals = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Goal goal = GoalConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                goals.Add(goal);
            }

            return goals;
        }

        public async Task<List<Goal>> GetAllByHolidayId(string holidayId)
        {
            CollectionReference docRef = _db.Collection("goals");
            Query query = docRef.WhereEqualTo("holidayId", holidayId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Goal> goals = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Goal goal = GoalConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                goals.Add(goal);
            }

            return goals;
        }

        public async Task<Goal> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            Goal goal = GoalConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
            return goal;
        }

        public async Task Update(Goal item)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{item.Id}");
            Dictionary<string, object> goal = GoalConverter.FromModelToDictionary(item);
            await docRef.UpdateAsync(goal);
        }

        public async Task PatchGoalStatus(string goalId, string goalStatusId)
        {
            DocumentReference docRef = _db.Collection("goals").Document($"{goalId}");
            Dictionary<string, object> goal = new Dictionary<string, object>
            {
                {"goalStatusId", $"{goalStatusId}"}
            };

            await docRef.UpdateAsync(goal);
        }

        #endregion
    }
}
