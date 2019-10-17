using System.Runtime.Serialization;

namespace DaprCards.Cards
{
    [DataContract]
    public sealed class Card
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}