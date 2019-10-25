using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class UserDeck
    {
        [DataMember(Name = "deckId", IsRequired = true)]
        public string? DeckId { get; set; }
    }
}