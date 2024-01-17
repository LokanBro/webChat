using MongoDB.Bson;

namespace MongoAPI.Models
{
    public class UserRoles
    {
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
