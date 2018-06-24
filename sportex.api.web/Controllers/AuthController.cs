using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sportex.api.logic;
using sportex.api.web.DTO;

namespace sportex.api.web.Controllers
{
    [Route("api/auth")]
    public class AuthController: BaseController
    {

        IConfigurationRoot configuration;

        public AuthController()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {
            int accountId = validateUser(request.Username, request.Password);
            if (accountId != 0)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };

                string apiKey = configuration.GetValue<string>("apiSettings:apiKey");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "sportex.com",
                    audience: "sportex.com",
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: creds);

                return StatusCode(200, new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires = token.ValidTo,
                    accountId
                });
            }
            return BadRequest("Could not verify username and password");
        }

        private int validateUser(String username, String password)
        {
            AccountManager am = new AccountManager();
            return am.ValidateAccount(username, password);
        }
    }
}
