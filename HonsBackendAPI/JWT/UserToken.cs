using System.Security.Claims;

namespace HonsBackendAPI.JWT
{
    public class UserToken
    {
        public string? Token { get; set; }

        //customer start
        public string? Id { get; set; }

      


        public string? Email { get; set; }

        //public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
        public string? Role { get; set; }

        //public string? ConfirmPassword { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime UpdatedAt { get; set; }
        //cutomer end

        public TimeSpan Validate { get; set; }
        public string? RefreshToken { get; set; }
   
        public DateTime ExpiredTime { get; set; }
    }
}
