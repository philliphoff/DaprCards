using System.Threading.Tasks;
using Dapr.Actors;

namespace DaprCards.Users
{
    public interface IUserActor : IActor
    {
        Task<UserDetails> GetDetailsAsync();

        Task SetDetailsAsync(UserDetails details);
    }
}