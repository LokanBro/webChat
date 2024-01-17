using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoAPI.Models;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatConnection"));

            var dbList = dbClient.GetDatabase("Mongo").GetCollection<Chat>("Chat").AsQueryable();
            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Chat Chat)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatConnection"));

            int LastChatId = dbClient.GetDatabase("Mongo").GetCollection<Chat>("Chat").AsQueryable().Count();
            Chat.ChatId = LastChatId + 1;

            dbClient.GetDatabase("Mongo").GetCollection<Chat>("Chat").InsertOne(Chat);
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Chat Chat)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatConnection"));

            var filter = Builders<Chat>.Filter.Eq("ChatId", Chat.ChatId);

            var update = Builders<Chat>.Update.Set("ChatId", Chat.ChatId);


            dbClient.GetDatabase("Mongo").GetCollection<Chat>("Chat").UpdateOne(filter, update);
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatConnection"));

            var filter = Builders<Chat>.Filter.Eq("ChatId", id);

            dbClient.GetDatabase("Mongo").GetCollection<Chat>("Chat").DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
