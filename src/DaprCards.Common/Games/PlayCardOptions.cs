using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class PlayCardOptions
    {
        [DataMember(Name = "cardId", IsRequired = true)]
        public string? CardId { get; set; }

        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }
    }
}