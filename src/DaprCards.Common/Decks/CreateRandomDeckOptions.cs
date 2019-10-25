using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DaprCards.Decks
{
    [DataContract]
    public sealed class CreateRandomDeckOptions
    {
        [DataMember(Name = "count")]
        public int? Count { get; set; }

        [DataMember(Name = "name")]
        public string? Name { get; set; }

        [DataMember(Name = "userId", IsRequired = true)]
        public string? UserId { get; set; }
    }
}