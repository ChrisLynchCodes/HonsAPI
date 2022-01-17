namespace HonsBackendAPI.JWT
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; } = null!;
        public bool ValidateIssuer { get; set; } = true;
        public string ValidIssuer { get; set; } = null!;
        public bool ValidateAudience { get; set; } = true;
        public string ValidAudience { get; set; } = null!;
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
    }
}
