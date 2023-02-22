using System;
using MP.Lib.Contrains;
using MP.Lib.Core.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MP.Lib.Models
{
     [CollectionName("users")]
    public class User : IObject
    {

        public int account_id { get; set; }

        public int partner_id { get; set; }

        public int acceptance_point_id { get; set; }

        public string account_name { get; set; }

        public string full_name { get; set; }

        public string description { get; set; }

        public int is_admin { get; set; }

        public int is_active { get; set; }

        public DateTime created_on { get; set; }

        public string created_by { get; set; }

        public string password { get; set; }
    }
 //   [BsonIgnoreExtraElements]
 //   [CollectionName("users")]
 //   public class User : IObject
 //   {
 //       public User()
 //       {
 //           Created = DateTime.Now;
 //           Status = (int)UserStatus.Pending;
 //           RoleId = ObjectId.Empty;
 //       }
 //       [BsonElement("uid")]
 //       public string UserName { get; set; }

 //       [BsonElement("rid")]
 //       public ObjectId RoleId { get; set; }

 //       /* [BsonIgnoreIfNull]
 //        [BsonElement("money")]
 //        public Money Money { get; set; }
 //*/
 //       [BsonElement("pwd")]
 //       public string Pwd { get; set; }

 //       [BsonElement("email")]
 //       public string Email { get; set; }

 //       // email or username
 //       [BsonIgnoreIfNull]
 //       [BsonElement("refer")]
 //       public string Refer { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("fname")]
 //       public string FullName { get; set; }

 //       [BsonElement("ava")]
 //       public string Avatar { get; set; }

 //       [BsonElement("sex")]
 //       public int Sex { get; set; }

 //       [BsonElement("status")]
 //       public int Status { get; set; }

 //       [BsonElement("secret")]
 //       public string SecretKey { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("bth")]
 //       public DateTime? Birthday { get; set; }

 //       [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
 //       [BsonElement("crt")]
 //       public DateTime Created { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("actived")]
 //       public DateTime? Actived { get; set; }

 //       // Số chứng minh thư
 //       [BsonIgnoreIfNull]
 //       [BsonElement("cid")]
 //       public string CardId { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("province")]
 //       public string Province { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("add")]
 //       public string Address { get; set; }


 //       [BsonIgnoreIfNull]
 //       [BsonElement("mobi")]
 //       public string Mobi { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("tel")]
 //       public string Tel { get; set; }

 //       [BsonIgnoreIfNull]
 //       [BsonElement("yid")]
 //       public string YahooId { get; set; }

 //       //ID Ngan hang
 //       [BsonIgnoreIfNull]
 //       [BsonElement("bankid")]
 //       public string BankId { get; set; }

 //       //Tai khoan Ngan hang
 //       [BsonIgnoreIfNull]
 //       [BsonElement("bankacc")]
 //       public string BankAcc { get; set; }

 //       [BsonElement("ag")]
 //       public bool IsAgency { get; set; }

 //       [BsonElement("cityid")]
 //       public int CityId { get; set; }

 //       [BsonElement("cityname")]
 //       public string CityName { get; set; }

 //       [BsonElement("districtid")]
 //       public int DistrictId { get; set; }

 //       [BsonElement("districtname")]
 //       public string DistrictName { get; set; }

 //       [BsonElement("lingoId")]
 //       public int LingoId { get; set; }
 //   }
}