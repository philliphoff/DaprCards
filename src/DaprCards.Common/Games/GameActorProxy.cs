using System;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace DaprCards.Games
{
    public sealed class GameActorProxy : IGameActor
    {
        public static IGameActor CreateProxy(string id)
        {
            return new GameActorProxy(ActorProxy.Create(new ActorId(id), "GameActor"));
        }

        private readonly ActorProxy proxy;

        private GameActorProxy(ActorProxy proxy)
        {
            this.proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        #region IGameActor Members

        public Task<GameDetails> GetDetailsAsync()
        {
            return proxy.InvokeAsync<GameDetails>(nameof(IGameActor.GetDetailsAsync));
        }

        public Task<GameDetails> PlayCardAsync(PlayCardOptions options)
        {
            return proxy.InvokeAsync<GameDetails>(nameof(IGameActor.PlayCardAsync), options);
        }

        public Task SetDetailsAsync(GameDetails details)
        {
            return proxy.InvokeAsync(nameof(IGameActor.SetDetailsAsync), details);
        }

        #endregion
    }
}