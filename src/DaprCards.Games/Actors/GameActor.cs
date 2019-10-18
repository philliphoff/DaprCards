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

        public Task SetDetailsAsync(GameDetails details)
        {
            return this.StateManager.SetStateAsync(DetailsStateName, details);
        }
    }
}