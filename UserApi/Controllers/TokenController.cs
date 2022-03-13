﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration { get; set; }
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult RequestToken()
        {
            try
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, "Berger")
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                        );

                SigningCredentials creds = new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                   issuer: "UserApi.net",
                   audience: "UserApi.net",
                   claims: claims,
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: creds
                );

                string tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}