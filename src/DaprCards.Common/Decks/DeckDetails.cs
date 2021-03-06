using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DaprCards.Decks
{
    [DataContract]
    public sealed class DeckDetails
    {
        [DataMember(Name = "cards")]
        public DeckCard[]? Cards { get; set; }

        [DataMember(Name = "name")]
        public string? Name { get; set; }

        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }
    }
}