using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class CreateUserOptions
    {
        [DataMember(Name = "name")]
        public string? Name { get; set; }
    }
}