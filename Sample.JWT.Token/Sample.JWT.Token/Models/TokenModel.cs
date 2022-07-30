using System.Text.Json.Serialization;

namespace Sample.JWT.Token.Models
{
    public class TokenModel
    {
        [JsonPropertyName("jwtToken")]
        public string JwtToken { get; set; }
    }
}
