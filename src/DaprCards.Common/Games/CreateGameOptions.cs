using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class CreateGameOptions
    {
        [DataMember(Name = "deckId", IsRequired = true)]
        public string? DeckId { get; set; }

        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }
    }
}