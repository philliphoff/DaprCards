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
using DaprCards.Decks;
using DaprCards.Users;

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

        [HttpPost]
        public async Task<string> CreateGameAsync([FromBody] CreateGameOptions options, [FromServices] StateClient state)
        {
            // TODO: Verify user owns the deck.

            string id = Guid.NewGuid().ToString();

            var deck = DeckActorProxy.CreateProxy(options.DeckId);

            var deckDetails = await deck.GetDetailsAsync();

            var game = GameActorProxy.CreateProxy(id);

            await game.SetDetailsAsync(
                new GameDetails
                {
                    Players =
                        new[]
                        {
                            new GamePlayer
                            {
                                Cards = deckDetails.Cards.Select(card => new GameCard { CardId = card.CardId }).ToArray(),
                                UserId = options.UserId
                            }
                        }
                });

            var games = await state.GetStateAsync<HashSet<string>>("games");

            games ??= new HashSet<string>();

            games.Add(id);

            await state.SaveStateAsync("games", games);

            var user = UserActorProxy.CreateProxy(options.UserId);

            await user.AddGameAsync(id);

            return id;
        }

        [HttpGet("{id}")]
        public Task<GameDetails> GetGameAsync(string id)
        {
            string actorType = "GameActor";
            var actorId = new ActorId(id);

            var actorProxy = ActorProxy.Create<IGameActor>(actorId, actorType);

            return actorProxy.GetDetailsAsync();
        }
    }
}
