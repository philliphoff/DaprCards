using System.Runtime.Serialization;

namespace DaprCards.Decks
{
    [DataContract]
    public sealed class DeckCard
    {
        [DataMember(Name = "cardId")]
        public string? CardId { get; set; }
    }
}