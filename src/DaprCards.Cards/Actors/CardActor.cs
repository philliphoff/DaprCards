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

        public async Task<bool> SetDetailsAsync(CardDetails details)
        {
            await this.StateManager.SetStateAsync(DetailsStateName, details);

            return true;
        }
    }
}