using MP.Account.Core.Repo;
using MP.Lib.Core;
using MP.Lib.Core.MongoDb;
using MP.Lib.Core.SSOCode;
using MP.Lib.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MP.Lib.Repo
{
    public class UserRepo : IRepository<User>
    {
        public static UserRepo Instant
        {
            get { return Lazy.Create<UserRepo>(); }
        }

        public void Remove(ObjectId id)
        {
            MainDb.Instant.Remove<User>(id);
        }

        public void Remove(string id)
        {
            Remove(ObjectId.Parse(id));
        }

        public void Remove(string[] id)
        {
            foreach (string _s in id)
            {
                Remove(_s);
            }
        }

        public User Info(ObjectId id)
        {
            return MainDb.Instant.GetById<User>(id);
        }

        public User Info(string id)
        {
            ObjectId _objId;

            ObjectId.TryParse(id, out _objId);

            return Info(_objId);
        }
        //public User InfoByEmail(string email)
        //{
        //    return MainDb.Instant.FindOne<User>(Query.Or(Query<User>.EQ(x => x.Email, email), Query<User>.EQ(x => x.UserName, email)));
        //}
        //public bool ExistsByEmail(string email)
        //{

        //    var _query = Query<User>.EQ(x => x.Email, email);
        //    return MainDb.Instant.FindOne<User>(_query) != null;

        //}
        //public User InfoByLingoId(int lingoId) {
        //    var _query = Query<User>.EQ(x => x.LingoId,lingoId);
        //    return MainDb.Instant.FindOne<User>(_query);
        //}

        //public void ChangePwd(string id, string password)
        //{
        //    var _update = Update<User>.Set(x => x.Pwd, password);
        //    MainDb.Instant.Update<User>(Query<User>.EQ(x => x.Id, ObjectId.Parse(id)), _update);
        //}

        public void Save(User obj)
        {
            MainDb.Instant.Save(obj);
        }

        //public UserSSOInfo AuthenticateUser(string UserName, string Password)
        //{
        //    UserSSOInfo _user = null;

        //    var _query = Query.And(
        //            Query<User>.EQ(x => x.UserName, UserName),
        //            Query<User>.EQ(x => x.Pwd, Password));

        //    var _data = MainDb.Instant.FindOne<User>(_query);
        //    if (_data != null)
        //    {
        //        _user = new UserSSOInfo
        //            {
        //                UserId = _data.Id.ToString(),
        //                UserName = _data.UserName,
        //                FullName = _data.FullName,
        //                Pwd = _data.Pwd,
        //                Email = _data.Email,
        //                IsAgency = _data.IsAgency,
        //                Token = Utility.GetGuidHash(),
        //                BankAcc = _data.BankAcc,
        //                BankId = _data.BankId
        //            };
        //    }
        //    return _user;
        //}

        //public UserSSOInfo GetUserInfoByUniqueId(string id)
        //{
        //    UserSSOInfo _user = null;
        //    ObjectId _objId;
        //    ObjectId.TryParse(id, out _objId);
        //    var _data = Info(_objId);
        //    if (_data != null)
        //    {
        //        _user = new UserSSOInfo
        //        {
        //            UserId = _data.Id.ToString(),
        //            UserName = _data.UserName,
        //            FullName = _data.FullName,
        //            Pwd = _data.Pwd,
        //            Email = _data.Email,
        //            IsAgency = _data.IsAgency,
        //            Token = Utility.GetGuidHash(),
        //            BankAcc = _data.BankAcc,
        //            BankId = _data.BankId
        //        };
        //    }
        //    return _user;
        //}
    }
}