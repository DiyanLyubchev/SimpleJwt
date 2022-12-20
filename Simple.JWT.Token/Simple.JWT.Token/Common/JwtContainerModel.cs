using Microsoft.IdentityModel.Tokens;

namespace Sample.JWT.Token.Common;

public static class JwtContainerModel
{
    public static int ExpireHoures { get; } = 1;

    public static string SecurityAlgorithm { get; } = SecurityAlgorithms.HmacSha256Signature;

    public static string Key { get; } = "This is my test security key";
}

