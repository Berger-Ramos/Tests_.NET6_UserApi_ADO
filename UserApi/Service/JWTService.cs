using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Service.ServiceInterface;

namespace UserApi.Service
{
    public class JWTService : ITokenService
    {
        private string TokenKey { get; set; }
        private List<Claim> TokenClaims { get; set; }
        public JWTService(string _tokenKey)
        {
            TokenKey = _tokenKey;
        }

        public void AddClaims(IDictionary<string,string> claims)
        {
            if (claims != null)
            {
                TokenClaims = new List<Claim>();

                foreach (var claim in claims)
                {
                    TokenClaims.Add(new Claim(claim.Key, claim.Value));
                }
            }
        }

        public string GanerateToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(TokenKey)
                        );

            SigningCredentials creds = new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
               issuer: "UserApi.net",
               audience: "UserApi.net",
               claims: TokenClaims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: creds
            );

            string tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenResponse;
        }

    }
}
