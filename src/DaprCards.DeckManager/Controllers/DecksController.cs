using DaprCards.Decks;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprCards.DeckManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly ILogger<DecksController> logger;

        public DecksController(ILogger<DecksController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync([FromServices] StateClient state)
        {
            var decks = await state.GetStateAsync<HashSet<string>>("decks");

            return decks ?? Enumerable.Empty<string>();
        }

        [HttpGet("{id}")]
        public Task<DeckDetails> GetDeckAsync(string id)
        {
            string actorType = "DeckActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IDeckActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }

        [HttpPut("{id}")]
        public async Task SetDeckAsync(string id, [FromBody] DeckDetails details, [FromServices] StateClient state)
        {
            string actorType = "DeckActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IDeckActor>(actorId, actorType);

            await actorProxy.SetDetailsAsync(details);

            var decks = await state.GetStateAsync<HashSet<string>>("decks");

            decks ??= new HashSet<string>();

            decks.Add(id);

            await state.SaveStateAsync("decks", decks);
        }
    }
}
