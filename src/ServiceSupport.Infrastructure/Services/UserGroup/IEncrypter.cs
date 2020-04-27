namespace ServiceSupport.Infrastructure.Services.UserGroup
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}