using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Users
{
    public interface IUserActor : IActor
    {
        Task AddCardAsync(string cardId);

        Task AddDeckAsync(string deckId);

        Task<UserDetails> GetDetailsAsync();

        Task SetDetailsAsync(UserDetails details);
    }
}