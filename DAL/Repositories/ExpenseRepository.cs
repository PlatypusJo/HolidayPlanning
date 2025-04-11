using DAL.Abstract;
using DAL.Converters;
using DAL.Entities;
using DAL.Interfaces;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Репозиторий Статьи расходов.
    /// </summary>
    public class ExpenseRepository : BaseRepository, IExpenseRepository
    {
        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста.
        /// </summary>
        /// <param name="db"> Контекст базы данных. </param>
        public ExpenseRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Expense item)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{item.Id}");
            Dictionary<string, object> expense = ExpenseConverter.FromModelToDictionary(item);
            await docRef.SetAsync(expense);
        }

        public async Task<bool> Delete(string id)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{id}");
            return await docRef.DeleteAsync() is not null;
        }

        public async Task<bool> Exists(string id)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{id}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task<List<Expense>> GetAll()
        {
            CollectionReference expensesRef = _db.Collection("expenses");
            QuerySnapshot snapshot = await expensesRef.GetSnapshotAsync();

            List<Expense> expenses = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var expense = ExpenseConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                expenses.Add(expense);
            }

            return expenses;
        }

        public async Task<List<Expense>> GetAllByHolidayId(string holidayId)
        {
            CollectionReference docRef = _db.Collection("expenses");
            Query query = docRef.WhereEqualTo("holidayId", holidayId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Expense> expenses = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var expense = ExpenseConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                expenses.Add(expense);
            }

            return expenses;
        }

        public async Task<Expense> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            Expense expense = ExpenseConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
            return expense;
        }

        public async Task Update(Expense item)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{item.Id}");
            Dictionary<string, object> expense = ExpenseConverter.FromModelToDictionary(item);
            await docRef.UpdateAsync(expense);
        }

        public async Task PatchPaid(string expenseId, double paid)
        {
            DocumentReference docRef = _db.Collection("expenses").Document($"{expenseId}");
            Dictionary<string, object> expense = new Dictionary<string, object>
            {
                {"paid", $"{paid}"}
            };

            await docRef.UpdateAsync(expense);
        }

        #endregion
    }
}