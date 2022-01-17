namespace HonsBackendAPI.JWT
{
    public class UserToken
    {
        public string? Token { get; set; }

        //customer start
        public string? Sub { get; set; }

        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string? ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        //cutomer end

        public TimeSpan Validaty { get; set; }
        public string? RefreshToken { get; set; }
   
        public DateTime ExpiredTime { get; set; }
    }
}
