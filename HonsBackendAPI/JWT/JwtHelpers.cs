using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HonsBackendAPI.JWT
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userToken, string Id)
        {

            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userToken.Sub.ToString()),
                   
                    new Claim(ClaimTypes.Email, userToken.Email),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }


        public static IEnumerable<Claim> GetClaims(this UserToken userToken, out string Id)
        {
            Id = Guid.NewGuid().ToString();
            return GetClaims(userToken, Id);
        }


        public static UserToken GenTokenkey(UserToken model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserToken();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                string Id = Guid.Empty.ToString();
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, out Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.Email = model.Email;
                UserToken.Sub = model.Sub;
              
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
