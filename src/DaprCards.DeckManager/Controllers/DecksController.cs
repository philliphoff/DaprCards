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

        [HttpPost("createRandomDeck")]
        public async Task<DeckDetails> CreateRandomDeckAsync([FromBody] CreateRandomDeckOptions options, [FromServices] StateClient state)
        {
            string id = Guid.NewGuid().ToString();

            int count = options.Count ?? 10;

            var details = new DeckDetails
            {
                Cards = new DeckCard[count],
                UserId = options.UserId
            };

            // TODO: Choose an appropriate seed.
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // TODO: Card manager should manage generation of IDs.
                int cardValue = random.Next(1, 100 + 1);

                string daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
                string daprUrl = $"http://localhost:{daprPort}/v1.0";
                string cardUrl = $"{daprUrl}/invoke/{Constants.AppIds.CardManager}/method/cards";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

                    var response = await client.PostAsync(
                        cardUrl,
                        new StringContent(
                            JsonSerializer.Serialize(
                                new CardDetails
                                {
                                    UserId = options.UserId,
                                    Value = cardValue
                                }),
                            Encoding.UTF8,
                            MediaTypeNames.Application.Json));

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    string cardId = JsonSerializer.Deserialize<string>(responseBody);

                    details.Cards[i] = new DeckCard{ CardId = cardId };
                }
            }

            await this.SetDeckAsync(id, details, state);

            return details;
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
