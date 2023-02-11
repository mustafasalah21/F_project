namespace ULearn.infrastructure
{
    public interface IConfigurationSettings
    {
        string JwtKey { get; }

        string Issuer { get; }
    }
}
