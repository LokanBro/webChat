using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoAPI.Models
{

    public class User
    {
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int UserRoleId { get; set; }
        public bool UserBan { get; set; }
    }
}
