using Dapr.Actors;
using Dapr.Actors.Client;
using DaprCards.Cards;
using System;

namespace DaprCards.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorId = new ActorId("3");

            var details = new CardDetails
            {
                UserId = "42",
                Value = 84
            };

            // Make strongly typed Actor calls with Remoting.
            // DemoACtor is the type registered with Dapr runtime in the service.
            var proxy = ActorProxy.Create<ICardActor>(actorId, "CardActor");
            System.Console.WriteLine("Making call using actor proxy to save data.");
            proxy.SetDetailsAsync(details).GetAwaiter().GetResult();
        }
    }
}
