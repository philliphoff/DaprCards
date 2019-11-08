using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GameCard
    {
        [DataMember(Name = "cardId", IsRequired = true)]
        public string? CardId { get; set; }

        [DataMember(Name = "isPlayed")]
        public bool IsPlayed { get; set; }

        [DataMember(Name = "value", IsRequired = true)]
        public int Value { get; set; }
    }
}