using DaprCards.Cards;
using DaprCards.Decks;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.IO;
using DaprCards.Users;

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
            var deck = DeckActorProxy.CreateProxy(id);

            return deck.GetDetailsAsync();
        }

        [HttpPost("createRandomDeck")]
        public async Task<string> CreateRandomDeckAsync([FromBody] CreateRandomDeckOptions options, [FromServices] StateClient state)
        {
            if (options.UserId == null)
            {
                throw new ArgumentException("UserId should be non-null.", nameof(options));
            }

            string id = Guid.NewGuid().ToString();

            int count = options.Count ?? 10;

            var details = new DeckDetails
            {
                Cards = new DeckCard[count],
                UserId = options.UserId
            };

            // TODO: Choose an appropriate seed.
            var random = new Random();

            using var cardManager = CardManagerProxy.CreateProxy();

            for (int i = 0; i < count; i++)
            {
                // TODO: Card manager should manage generation of IDs.
                int cardValue = random.Next(1, 100 + 1);

                string cardId = await cardManager.CreateCardAsync(
                    new CardDetails
                    {
                        UserId = options.UserId,
                        Value = cardValue
                    });

                details.Cards[i] = new DeckCard{ CardId = cardId };
            }

            await this.SetDeckAsync(id, details, state);

            var user = UserActorProxy.CreateProxy(details.UserId);

            await user.AddDeckAsync(id);

            return id;
        }

        [HttpPut("{id}")]
        public async Task SetDeckAsync(string id, [FromBody] DeckDetails details, [FromServices] StateClient state)
        {
            var deck = DeckActorProxy.CreateProxy(id);

            await deck.SetDetailsAsync(details);

            var decks = await state.GetStateAsync<HashSet<string>>("decks");

            decks ??= new HashSet<string>();

            decks.Add(id);

            await state.SaveStateAsync("decks", decks);
        }
    }
}
