using System.Text.Json.Serialization;

namespace Sample.JWT.Token.Models
{
    public class UserCred
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
