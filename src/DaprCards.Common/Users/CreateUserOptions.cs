using System.Runtime.Serialization;

namespace DaprCards.Users
{
    [DataContract]
    public sealed class CreateUserOptions
    {
        [DataMember(Name = "email", IsRequired = true)]
        public string? Email { get; set; }

        [DataMember(Name = "name")]
        public string? Name { get; set; }
    }
}