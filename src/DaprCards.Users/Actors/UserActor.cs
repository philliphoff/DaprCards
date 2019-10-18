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