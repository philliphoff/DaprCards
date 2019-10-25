using System;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace DaprCards.Decks
{
    public sealed class DeckActorProxy : IDeckActor
    {
        public static IDeckActor CreateProxy(string id)
        {
            return new DeckActorProxy(ActorProxy.Create(new ActorId(id), "DeckActor"));
        }

        private readonly ActorProxy proxy;

        private DeckActorProxy(ActorProxy proxy)
        {
            this.proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        #region IDeckActor Members

        public Task<DeckDetails> GetDetailsAsync()
        {
            return proxy.InvokeAsync<DeckDetails>(nameof(IDeckActor.GetDetailsAsync));
        }

        public Task SetDetailsAsync(DeckDetails details)
        {
            return proxy.InvokeAsync(nameof(IDeckActor.SetDetailsAsync), details);
        }

        #endregion
    }
}