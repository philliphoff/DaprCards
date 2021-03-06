using System.Runtime.Serialization;

namespace DaprCards.Cards
{
    [DataContract]
    public sealed class CardDetails
    {
        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }

        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}