using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Runtime;

namespace DaprCards.Decks.Actors
{
    internal sealed class DeckActor : Actor, IDeckActor
    {
        private const string DetailsStateName = "details";

        public DeckActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<DeckDetails> GetDetailsAsync()
        {
            return this.StateManager.GetStateAsync<DeckDetails>(DetailsStateName);
        }

        public Task SetDetailsAsync(DeckDetails details)
        {
            return this.StateManager.SetStateAsync(DetailsStateName, details);
        }
    }
}