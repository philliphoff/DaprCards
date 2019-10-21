using DaprCards.Games;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprCards.GameManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> logger;

        public GamesController(ILogger<GamesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync([FromServices] StateClient state)
        {
            var games = await state.GetStateAsync<HashSet<string>>("games");

            return games ?? Enumerable.Empty<string>();
        }

        [HttpGet("{id}")]
        public Task<GameDetails> GetGameAsync(string id)
        {
            string actorType = "GameActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IGameActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }

        [HttpPut("{id}")]
        public async Task SetGameAsync(string id, [FromBody] GameDetails details, [FromServices] StateClient state)
        {
            string actorType = "GameActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IGameActor>(actorId, actorType);

            await actorProxy.SetDetailsAsync(details);

            var games = await state.GetStateAsync<HashSet<string>>("games");

            games ??= new HashSet<string>();

            games.Add(id);

            await state.SaveStateAsync("games", games);
        }
    }
}
