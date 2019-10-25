using System.Threading;
using System.Threading.Tasks;

namespace DaprCards.Cards
{
    public interface ICardManager
    {
        Task<string> CreateCardAsync(CardDetails details, CancellationToken cancellationToken = default);
    }
}