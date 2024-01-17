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
    public class UserRolesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserRolesController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get(int? userId = null)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserRolesConnection"));
            var collection = dbClient.GetDatabase("Mongo").GetCollection<UserRoles>("UserRoles");
            IMongoQueryable<UserRoles> dbList;

            if (userId != null)
            {
                dbList = collection.AsQueryable().Where(m => m.UserId == userId);
            }
            else
            {
                dbList = collection.AsQueryable();
            }
            return new JsonResult(dbList.ToList());
        }

        [HttpPost]
        public JsonResult Post(UserRoles UserRoles)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("UserRolesConnection"));

            int LastUserRolesId = dbClient.GetDatabase("Mongo").GetCollection<UserRoles>("UserRoles").AsQueryable().Count();
            UserRoles.RoleId= LastUserRolesId + 1;

            dbClient.GetDatabase("Mongo").GetCollection<UserRoles>("UserRoles").InsertOne(UserRoles);
            return new JsonResult("Added Successfully");
        }
    }
}
