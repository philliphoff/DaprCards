using System;
using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GameAction
    {
        [DataMember(Name = "description")]
        public string? Description { get; set; }

        [DataMember(Name = "timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }
}