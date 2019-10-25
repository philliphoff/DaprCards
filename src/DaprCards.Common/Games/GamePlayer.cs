using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GamePlayer
    {
        [DataMember(Name = "cards")]
        public GameCard[]? Cards { get; set; }

        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }
    }
}