using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class UserDetails
    {
        [DataMember(Name = "cards")]
        public UserCard[]? Cards { get; set; }

        [DataMember(Name = "decks")]
        public UserDeck[]? Decks { get; set; }

        [DataMember(Name = "email")]
        public string? Email { get; set; }

        [DataMember(Name = "name")]
        public string? Name { get; set; }
    }
}