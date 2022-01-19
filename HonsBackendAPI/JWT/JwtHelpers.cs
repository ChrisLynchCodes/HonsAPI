using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HonsBackendAPI.JWT
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userToken, string Id)
        {
            ICollection<string> Roles = new List<string>();
            switch (userToken.Role)
            {
                case not null when userToken.Role.Equals("SuperAdmin"):
                    Roles.Add("SuperAdmin");

                    break;
                case not null when userToken.Role.Equals("Admin"):
                    Roles.Add("Admin");
                    break;
                case not null when userToken.Role.Equals("Customer"):
                    Roles.Add("Customer");
                    break;

            }


            List<Claim> claims = new()
             {
                new Claim("Id", userToken.Id.ToString()),

                    
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
                    };
            
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
                
                // Get key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);


                string Id = Guid.Empty.ToString();

                //was validae
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.Validate = expireTime.TimeOfDay;
                
                var JWToken = new JwtSecurityToken
                    (
                    issuer: jwtSettings.ValidIssuer, 
                    audience: jwtSettings.ValidAudience, 
                    claims: GetClaims(model, out Id), 
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime, 
                    expires: new DateTimeOffset(expireTime).DateTime, 
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    );


                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.Email = model.Email;
                UserToken.Role = model.Role;
                UserToken.Id = model.Id;
                //UserToken.ConfirmPassword = model.ConfirmPassword;
                //UserToken.CreatedAt = model.CreatedAt;
                //UserToken.LastName = model.LastName;
                //UserToken.FirstName = model.FirstName;
                //UserToken.UpdatedAt = model.UpdatedAt;
                //UserToken.Claims = JWToken.Claims;
                //var claim = JWToken.Claims.Where(x => x.Value == "Customer");
                //UserToken.Role = claim.First().Value.ToString();
               
                

                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
