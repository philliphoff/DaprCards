using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprCards.Cards.Actors
{
    internal sealed class CardActor : Actor, ICardActor
    {
        private const string DetailsStateName = "details";

        public CardActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<CardDetails> GetDetailsAsync()
        {
            return this.StateManager.GetStateAsync<CardDetails>(DetailsStateName);
        }

        public Task SetDetailsAsync(CardDetails details)
        {
            return this.StateManager.SetStateAsync(DetailsStateName, details);
        }
    }
}