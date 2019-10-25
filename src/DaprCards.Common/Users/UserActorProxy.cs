using System;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace DaprCards.Users
{
    public sealed class UserActorProxy : IUserActor
    {
        public static IUserActor CreateProxy(string id)
        {
            return new UserActorProxy(ActorProxy.Create(new ActorId(id), "UserActor"));
        }

        private readonly ActorProxy proxy;

        private UserActorProxy(ActorProxy proxy)
        {
            this.proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        #region IUserActor Members

        public Task<UserDetails> GetDetailsAsync()
        {
            return proxy.InvokeAsync<UserDetails>(nameof(IUserActor.GetDetailsAsync));
        }

        public Task SetDetailsAsync(UserDetails details)
        {
            return proxy.InvokeAsync(nameof(IUserActor.SetDetailsAsync), details);
        }

        #endregion
    }
}