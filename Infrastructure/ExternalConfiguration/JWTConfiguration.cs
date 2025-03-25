namespace Persistence.ExternalConfiguration
{
    public class JWTConfiguration
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; }   = null!;
        public string Audience { get; set; } = null!;
        public int LoginDays { get; set; } = -1;
        public int OtpMinutes { get; set; } = -1;

    }
}
