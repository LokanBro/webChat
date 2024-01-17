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
    public class ChatUsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ChatUsersController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get(int? chatId = null)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatUsersConnection"));
            var collection = dbClient.GetDatabase("Mongo").GetCollection<ChatUsers>("ChatUsers");
            IMongoQueryable<ChatUsers> dbList;

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
        public JsonResult Post(ChatUsers ChatUsers)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("ChatUsersConnection"));

            dbClient.GetDatabase("Mongo").GetCollection<ChatUsers>("ChatUsers").InsertOne(ChatUsers);
            return new JsonResult("Added Successfully");
        }
    }
}
