using System.Runtime.Serialization;

namespace DaprCards.Games
{
    [DataContract]
    public sealed class GameDetails
    {
        [DataMember(Name = "history")]
        public GameAction[] History { get; set; }

        [DataMember(Name = "players")]
        public GamePlayer[] Players { get; set; }
    }
}