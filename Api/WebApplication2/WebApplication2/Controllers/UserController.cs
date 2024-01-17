using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoAPI.Models;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserConnection"));

            var dbList = dbClient.GetDatabase("Mongo").GetCollection<User>("User").AsQueryable();
            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserConnection"));

            int LastUserId = dbClient.GetDatabase("Mongo").GetCollection<User>("User").AsQueryable().Count();
            user.UserRoleId = Role.DefaultRoleId();
            user.UserId = LastUserId + 1;
            user.UserBan = false;

            dbClient.GetDatabase("Mongo").GetCollection<User>("User").InsertOne(user);
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserConnection"));

            var filter = Builders<User>.Filter.Eq("UserId", user.UserId);

            var update = Builders<User>.Update.Set("UserId", user.UserId)
                                                .Set("UserLogin", user.UserLogin)
                                                .Set("UserRoleId", user.UserRoleId)
                                                .Set("UserBan", user.UserBan)
                                                .Set("UserPassword", user.UserPassword);


            dbClient.GetDatabase("Mongo").GetCollection<User>("User").UpdateOne(filter, update);
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserConnection"));

            var filter = Builders<User>.Filter.Eq("UserId", id);

            dbClient.GetDatabase("Mongo").GetCollection<User>("User").DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
