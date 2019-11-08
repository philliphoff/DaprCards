using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class UserGame
    {
        [DataMember(Name = "gameId", IsRequired = true)]
        public string? GameId { get; set; }
    }
}