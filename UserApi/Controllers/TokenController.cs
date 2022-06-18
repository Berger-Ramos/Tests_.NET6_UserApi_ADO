using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Domain;
using UserApi.Domain.DomainInterface;
using UserApi.Service.ServiceInterface;
using UserApi.Utils.Inputs;

namespace UserApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration { get; set; }

        private ITokenService _tokenservice { get; set; }

        public TokenController(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenservice = tokenService;
        }
        
        [HttpPost]
        public IActionResult RequestToken(UserJsonInput userJsonInput)
        {
            try
            {
                IUserControl userControl = new UserControl(null);

                userControl.UserIsValid(userJsonInput);


                Dictionary<string,string> tokenClaim = new Dictionary<string,string>();
                tokenClaim.Add("Name", userJsonInput.Name);

                _tokenservice.AddClaims(tokenClaim);

                string tokenResponse = _tokenservice.GanerateToken();

                //var claims = new[]
                //{
                //new Claim(ClaimTypes.Name, userJsonInput.Name)
                //};

                //SymmetricSecurityKey key = new SymmetricSecurityKey(
                //        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                //        );

                //SigningCredentials creds = new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256);

                //var token = new JwtSecurityToken
                //(
                //   issuer: "UserApi.net",
                //   audience: "UserApi.net",
                //   claims: claims,
                //   expires: DateTime.Now.AddHours(1),
                //   signingCredentials: creds
                //);

                //string tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
