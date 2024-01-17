using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Linq;
using MongoAPI.Models;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get(int? chatId = null)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MessageConnection"));
            var collection = dbClient.GetDatabase("Mongo").GetCollection<Message>("Message");
            IMongoQueryable<Message> dbList;

            if (chatId != null)
            {
                dbList = collection.AsQueryable().Where(m => m.ChatId == chatId);
            }
            else
            {
                dbList = collection.AsQueryable();
            }
            return new JsonResult(dbList.ToList());
        }

        [HttpPost]
        public JsonResult Post(Message Message)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("MessageConnection"));

            int LastMessageId = dbClient.GetDatabase("Mongo").GetCollection<Message>("Message").AsQueryable().Count();
            Message.MessageId = LastMessageId + 1;

            dbClient.GetDatabase("Mongo").GetCollection<Message>("Message").InsertOne(Message);
            return new JsonResult("Added Successfully");
        }
    }
}
