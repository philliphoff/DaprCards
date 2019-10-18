using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GameCard
    {
        [DataMember(Name = "cardId")]
        public string CardId { get; set; }

        [DataMember(Name = "isPlayed")]
        public string IsPlayed { get; set; }
    }
}