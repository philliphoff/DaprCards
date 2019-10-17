using System.Runtime.Serialization;

namespace DaprCards.Cards
{
    [DataContract]
    public sealed class CardDetails
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}