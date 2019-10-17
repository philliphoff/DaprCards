using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Cards
{
    public interface ICardActor : IActor
    {
        Task<Card> GetDetailsAsync();

        Task SetDetailsAsync(Card card);
    }
}