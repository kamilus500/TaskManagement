namespace TaskManagement.Domain.Models.Configs
{
    public class AuthenticationSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public int TokenExpiryTimeInHour { get; set; }
        public string Secret { get; set; }
    }
}
