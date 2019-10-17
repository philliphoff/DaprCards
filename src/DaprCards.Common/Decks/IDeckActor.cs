using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Decks
{
    public interface IDeckActor : IActor
    {
        Task<DeckDetails> GetDetailsAsync();

        Task SetDetailsAsync(DeckDetails details);
    }
}