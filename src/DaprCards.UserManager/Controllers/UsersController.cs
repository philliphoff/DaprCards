using DaprCards.Users;
using Dapr;
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
        public async Task<IEnumerable<string>> GetAsync([FromServices] StateClient state)
        {
            var users = await state.GetStateAsync<HashSet<string>>("users");

            return users ?? Enumerable.Empty<string>();
        }

        [HttpGet("{id}")]
        public Task<UserDetails> GetUserAsync(string id)
        {
            string actorType = "UserActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IUserActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }

        [HttpPut("{id}")]
        public async Task SetUserAsync(string id, [FromBody] UserDetails details, [FromServices] StateClient state)
        {
            string actorType = "UserActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IUserActor>(actorId, actorType);

            await actorProxy.SetDetailsAsync(details);

            var users = await state.GetStateAsync<HashSet<string>>("users");

            users ??= new HashSet<string>();

            users.Add(id);

            await state.SaveStateAsync("users", users);
        }
    }
}
