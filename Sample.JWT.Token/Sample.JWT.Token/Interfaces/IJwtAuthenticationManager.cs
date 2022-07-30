namespace Sample.JWT.Token.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
