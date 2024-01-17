using MongoDB.Bson;
using System.Data;

namespace MongoAPI.Models
{
    public class Chat
    {
        public ObjectId Id { get; set; }
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public DateTime ChatCreateDate { get; set; }   
    }
}
