using MongoDB.Bson;

namespace MongoAPI.Models
{
    public class Message
    {
        public ObjectId Id { get; set; }
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int CreaterId { get; set; }
        public string MessageBody { get; set; }
    }
}
