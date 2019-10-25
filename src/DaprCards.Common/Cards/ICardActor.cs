using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Cards
{
    public interface ICardActor : IActor
    {
        Task<CardDetails> GetDetailsAsync();

        Task SetDetailsAsync(CardDetails details);
    }
}