using DaprCards.Users;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprCards.UserManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;

        public UsersController(ILogger<UsersController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => index.ToString())
            .ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<UserDetails> GetUserAsync(string id)
        {
            string actorType = "UserActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IUserActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }

        [HttpPut]
        [Route("{id}")]
        public Task SetUserAsync(string id, [FromBody] UserDetails details)
        {
            string actorType = "UserActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IUserActor>(actorId, actorType);

            return actorProxy.SetDetailsAsync(details);
        }
    }
}
