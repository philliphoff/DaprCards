using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprCards.Cards.Actors
{
    internal sealed class CardActor : Actor, ICardActor
    {
        private const string CardStateName = "card";

        public CardActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<Card> GetDetailsAsync()
        {
            return this.StateManager.GetStateAsync<Card>(CardStateName);
        }

        public Task SetDetailsAsync(Card card)
        {
            return this.StateManager.SetStateAsync(CardStateName, card);
        }
    }
}