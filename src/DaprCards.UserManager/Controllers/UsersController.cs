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

        [HttpPost]
        public async Task<string> CreateUserAsync(CreateUserOptions options, [FromServices] StateClient state)
        {
            string id = Guid.NewGuid().ToString();

            var user = UserActorProxy.CreateProxy(id);

            await user.SetDetailsAsync(
                new UserDetails
                {
                    Name = options.Name
                });

            var users = await state.GetStateAsync<HashSet<string>>("users");

            users ??= new HashSet<string>();

            users.Add(id);

            await state.SaveStateAsync("users", users);

            return id;
        }

        [HttpGet("{id}")]
        public Task<UserDetails> GetUserAsync(string id)
        {
            string actorType = "UserActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IUserActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }
    }
}
