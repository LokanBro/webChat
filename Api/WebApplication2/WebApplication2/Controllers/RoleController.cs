using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoAPI.Models;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("RoleConnection"));

            var dbList = dbClient.GetDatabase("Mongo").GetCollection<Role>("Role").AsQueryable();
            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Role Role)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("RoleConnection"));

            int LastRoleId = dbClient.GetDatabase("Mongo").GetCollection<Role>("Role").AsQueryable().Count();
            Role.RoleId = LastRoleId + 1;

            dbClient.GetDatabase("Mongo").GetCollection<Role>("Role").InsertOne(Role);
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Role Role)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("RoleConnection"));

            var filter = Builders<Role>.Filter.Eq("RoleId", Role.RoleId);

            var update = Builders<Role>.Update.Set("RoleId", Role.RoleId)
                                                .Set("RoleBody", Role.RoleName);


            dbClient.GetDatabase("Mongo").GetCollection<Role>("Role").UpdateOne(filter, update);
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("RoleConnection"));

            var filter = Builders<Role>.Filter.Eq("RoleId", id);

            dbClient.GetDatabase("Mongo").GetCollection<Role>("Role").DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
