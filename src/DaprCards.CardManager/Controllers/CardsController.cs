using DaprCards.Cards;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DaprCards.Users;

namespace DaprCards.CardManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> logger;

        public CardsController(ILogger<CardsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync([FromServices] StateClient state)
        {
            var Cards = await state.GetStateAsync<HashSet<string>>("cards");

            return Cards ?? Enumerable.Empty<string>();
        }

        [HttpPost]
        public async Task<string> CreateCardAsync([FromBody] CardDetails details, [FromServices] StateClient state)
        {
            string id = Guid.NewGuid().ToString();

            await this.SetCardAsync(id, details, state);

            var user = UserActorProxy.CreateProxy(details.UserId);

            await user.AddCardAsync(id);

            return id;
        }

        [HttpGet("{id}")]
        public Task<CardDetails> GetCardAsync(string id)
        {
            string actorType = "CardActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<ICardActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }

        [HttpPut("{id}")]
        public async Task SetCardAsync(string id, [FromBody] CardDetails details, [FromServices] StateClient state)
        {
            var proxy = CardActorProxy.CreateProxy(id);

            await proxy.SetDetailsAsync(details);

            var cards = await state.GetStateAsync<HashSet<string>>("cards");

            cards ??= new HashSet<string>();

            cards.Add(id);

            await state.SaveStateAsync("cards", cards);
        }
    }
}
