using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GameCard
    {
        [DataMember(Name = "cardId")]
        public string? CardId { get; set; }

        [DataMember(Name = "isPlayed")]
        public bool IsPlayed { get; set; }
    }
}