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
    /// Репозиторий гостя
    /// </summary>
    public class MemberRepository : BaseRepository, IMemberRepository
    {

        #region Конструкторы

        /// <summary>
        /// Конструктор с определением контекста
        /// </summary>
        /// <param name="db">Контекст базы данных</param>
        public MemberRepository(FirestoreDb db) : base(db) { }

        #endregion

        #region Методы

        public async Task Create(Member item)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{item.Id}");
            Dictionary<string, object> member = MemberConverter.FromModelToDictionary(item);
            await docRef.SetAsync(member);
        }

        public async Task<bool> Delete(string id)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{id}");
            return await docRef.DeleteAsync() is not null;
        }

        public async Task<bool> Exists(string id)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{id}");
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task<List<Member>> GetAll()
        {
            CollectionReference usersRef = _db.Collection("members");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

            List<Member> members = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var member = MemberConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                members.Add(member);
            }

            return members;
        }

        public async Task<List<Member>> GetAllByHolidayId(string holidayId)
        {
            CollectionReference docRef = _db.Collection("members");
            Query query = docRef.WhereEqualTo("holidayId", holidayId);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<Member> members = [];

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var member = MemberConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
                members.Add(member);
            }

            return members;
        }

        public async Task<Member> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            Member member = MemberConverter.FromDictionaryToModel(document.ToDictionary(), document.Id);
            return member;
        }

        public async Task Update(Member item)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{item.Id}");
            Dictionary<string, object> member = MemberConverter.FromModelToDictionary(item);
            await docRef.UpdateAsync(member);
        }

        public async Task PatchMemberStatus(string memberId, string memberStatusId)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{memberId}");
            Dictionary<string, object> member = new Dictionary<string, object>
            {
                {"memberStatusId", $"{memberStatusId}"}
            };

            await docRef.UpdateAsync(member);
        }

        #endregion
    
    }
}
