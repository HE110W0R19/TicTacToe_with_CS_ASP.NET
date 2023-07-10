using FirstWebApp.ServerDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FirstWebApp.Controllers
{
    public class ApiController : Controller
    {
        public Guid[] GetTables()
        {
            return Database.Tables.Keys.ToArray();
        }

        public Guid[] GetUsers()
        {
            return Database.Users.Keys.ToArray();
        }

        public class GetUserNameRequest
        {
            [JsonProperty]
            public Guid userGuid { get; set; }
        }

        [HttpPost]
        public string GetUserName([FromBody] GetUserNameRequest requestValue)
        {
            return Database.Users[requestValue.userGuid];
        }
    }
}
