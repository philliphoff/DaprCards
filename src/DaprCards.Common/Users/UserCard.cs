using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class UserCard
    {
        [DataMember(Name = "cardId")]
        public string? CardId { get; set; }
    }
}