using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprCards.Games.Actors
{
    internal sealed class GameActor : Actor, IGameActor
    {
        private const string DetailsStateName = "details";

        public GameActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<GameDetails> GetDetailsAsync()
        {
            return this.StateManager.GetStateAsync<GameDetails>(DetailsStateName);
        }

        public async Task<GameDetails> PlayCardAsync(PlayCardOptions options)
        {
            var details = await this.GetDetailsAsync();

            var player = details.Players.FirstOrDefault(p => p.UserId == options.UserId);

            if (player == null)
            {
                throw new InvalidOperationException("The player is not in this game.");
            }

            var card = player.Cards.FirstOrDefault(c => c.CardId == options.CardId);

            if (card == null)
            {
                throw new InvalidOperationException("The card is not part of the deck.");
            }

            if (card.IsPlayed)
            {
                throw new InvalidOperationException("The card is already played.");
            }

            card.IsPlayed = true;

            var history = details.History?.ToList() ?? new List<GameAction>();

            history.Add(
                new GameAction
                {
                    Description = $"{options.UserId} played the card {options.CardId}.",
                    Timestamp = DateTimeOffset.Now
                });

            details.History = history.ToArray();

            await this.SetDetailsAsync(details);

            return details;
        }

        public Task SetDetailsAsync(GameDetails details)
        {
            return this.StateManager.SetStateAsync(DetailsStateName, details);
        }
    }
}