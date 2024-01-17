using MongoDB.Bson;

namespace MongoAPI.Models
{
    public class ChatUsers
    {
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
    }
}
