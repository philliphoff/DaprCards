using System;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace DaprCards.Cards
{
    public sealed class CardActorProxy : ICardActor
    {
        public static ICardActor CreateProxy(string id)
        {
            return new CardActorProxy(ActorProxy.Create(new ActorId(id), "CardActor"));
        }

        private readonly ActorProxy proxy;

        private CardActorProxy(ActorProxy proxy)
        {
            this.proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        #region ICardActor Members

        public Task<CardDetails> GetDetailsAsync()
        {
            return proxy.InvokeAsync<CardDetails>(nameof(ICardActor.GetDetailsAsync));
        }

        public Task SetDetailsAsync(CardDetails details)
        {
            return proxy.InvokeAsync(nameof(ICardActor.SetDetailsAsync), details);
        }

        #endregion
    }
}