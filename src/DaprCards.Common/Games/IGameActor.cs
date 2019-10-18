using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Games
{
    public interface IGameActor : IActor
    {
        Task<GameDetails> GetDetailsAsync();

        Task SetDetailsAsync(GameDetails details);
    }
}