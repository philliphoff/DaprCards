using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprCards.Users.Actors
{
    internal sealed class UserActor : Actor, IUserActor
    {
        private const string DetailsStateName = "details";

        public UserActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task AddCardAsync(string cardId)
        {
            var details = await this.GetDetailsAsync();

            var cards = details.Cards?.ToList();

            cards ??= new List<UserCard>();

            cards.Add(new UserCard { CardId = cardId });

            details.Cards = cards.ToArray();

            await this.SetDetailsAsync(details);
        }

        public async Task AddDeckAsync(string deckId)
        {
            var details = await this.GetDetailsAsync();

            var decks = details.Decks?.ToList();

            decks ??= new List<UserDeck>();

            decks.Add(new UserDeck { DeckId = deckId });

            details.Decks = decks.ToArray();

            await this.SetDetailsAsync(details);
        }

        public async Task AddGameAsync(string gameId)
        {
            var details = await this.GetDetailsAsync();

            var games = details.Games?.ToList();

            games ??= new List<UserGame>();

            games.Add(new UserGame { GameId = gameId });

            details.Games = games.ToArray();

            await this.SetDetailsAsync(details);
        }

        public Task<UserDetails> GetDetailsAsync()
        {
            return this.StateManager.GetStateAsync<UserDetails>(DetailsStateName);
        }

        public Task SetDetailsAsync(UserDetails details)
        {
            return this.StateManager.SetStateAsync(DetailsStateName, details);
        }
    }
}