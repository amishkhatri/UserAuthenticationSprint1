namespace xp.auth.core.integration.Domain
{
    public interface IAuthentication
    {
        bool Success { get; set; }

        bool Authenticated(User obj);
        
    }
}
