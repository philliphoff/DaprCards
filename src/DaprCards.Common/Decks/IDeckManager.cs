using System.Threading;
using System.Threading.Tasks;

namespace DaprCards.Decks
{
    public interface IDeckManager
    {
        Task<string> CreateRandomDeckAsync(CreateRandomDeckOptions options, CancellationToken cancellationToken = default);
    }
}