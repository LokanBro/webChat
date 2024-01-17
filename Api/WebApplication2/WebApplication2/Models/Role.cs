using MongoDB.Bson;

namespace MongoAPI.Models
{
    public class Role
    {
        public static int DefaultRoleId()
        {
            return 2;
        }
        public ObjectId Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
