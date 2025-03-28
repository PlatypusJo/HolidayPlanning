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
            Dictionary<string, object> member = new Dictionary<string, object>
            {
                {"holidayId", $"{item.HolidayId}"},
                {"memberCategoryId", $"{item.MemberCategoryId}"},
                {"memberStatusId", $"{item.MemberStatusId}"},
                {"menuCategoryId", $"{item.MenuCategoryId}"},
                {"fio", $"{item.FIO}"},
                {"phoneNumber", $"{item.PhoneNumber}"},
                {"email", $"{item.Email}"},
                {"comment", $"{item.Comment}"},
                {"isChild", $"{item.IsChild}"},
                {"isMale", $"{item.IsMale}"},
                {"seat", $"{item.Seat}"},
            };
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
                var documentTemp = document.ToDictionary();
                members.Add(new Member()
                {
                    Id = document.Id,
                    HolidayId = documentTemp["holidayId"].ToString(),
                    MemberCategoryId = documentTemp["memberCategoryId"].ToString(),
                    MemberStatusId = documentTemp["memberStatusId"].ToString(),
                    MenuCategoryId = documentTemp["menuCategoryId"].ToString(),
                    FIO = documentTemp["fio"].ToString(),
                    PhoneNumber = documentTemp["phoneNumber"].ToString(),
                    Email = documentTemp["email"].ToString(),
                    Comment = documentTemp["comment"].ToString(),
                    IsChild = Convert.ToBoolean(documentTemp["isChild"].ToString()),
                    IsMale = Convert.ToBoolean(documentTemp["isMale"].ToString()),
                    Seat = documentTemp["seat"].ToString(),
                });
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
                var documentTemp = document.ToDictionary();
                members.Add(new Member()
                {
                    Id = document.Id,
                    HolidayId = documentTemp["holidayId"].ToString(),
                    MemberCategoryId = documentTemp["memberCategoryId"].ToString(),
                    MemberStatusId = documentTemp["memberStatusId"].ToString(),
                    MenuCategoryId = documentTemp["menuCategoryId"].ToString(),
                    FIO = documentTemp["fio"].ToString(),
                    PhoneNumber = documentTemp["phoneNumber"].ToString(),
                    Email = documentTemp["email"].ToString(),
                    Comment = documentTemp["comment"].ToString(),
                    IsChild = Convert.ToBoolean(documentTemp["isChild"].ToString()),
                    IsMale = Convert.ToBoolean(documentTemp["isMale"].ToString()),
                    Seat = documentTemp["seat"].ToString(),
                });
            }

            return members;
        }

        public async Task<Member> GetItem(string id)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{id}");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            var documentTemp = document.ToDictionary();

            Member member = new Member()
            {
                Id = document.Id,
                HolidayId = documentTemp["holidayId"].ToString(),
                MemberCategoryId = documentTemp["memberCategoryId"].ToString(),
                MemberStatusId = documentTemp["memberStatusId"].ToString(),
                MenuCategoryId = documentTemp["menuCategoryId"].ToString(),
                FIO = documentTemp["fio"].ToString(),
                PhoneNumber = documentTemp["phoneNumber"].ToString(),
                Email = documentTemp["email"].ToString(),
                Comment = documentTemp["comment"].ToString(),
                IsChild = Convert.ToBoolean(documentTemp["isChild"].ToString()),
                IsMale = Convert.ToBoolean(documentTemp["isMale"].ToString()),
                Seat = documentTemp["seat"].ToString(),
            };

            return member;
        }

        public async Task Update(Member item)
        {
            DocumentReference docRef = _db.Collection("members").Document($"{item.Id}");
            Dictionary<string, object> member = new Dictionary<string, object>
            {
                {"holidayId", $"{item.HolidayId}"},
                {"memberCategoryId", $"{item.MemberCategoryId}"},
                {"memberStatusId", $"{item.MemberStatusId}"},
                {"menuCategoryId", $"{item.MenuCategoryId}"},
                {"fio", $"{item.FIO}"},
                {"phoneNumber", $"{item.PhoneNumber}"},
                {"email", $"{item.Email}"},
                {"comment", $"{item.Comment}"},
                {"isChild", $"{item.IsChild}"},
                {"isMale", $"{item.IsMale}"},
                {"seat", $"{item.Seat}"},
            };

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
