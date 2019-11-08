using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Games
{
    public interface IGameActor : IActor
    {
        Task<GameDetails> GetDetailsAsync();

        Task<GameDetails> PlayCardAsync(PlayCardOptions options);

        Task SetDetailsAsync(GameDetails details);
    }
}