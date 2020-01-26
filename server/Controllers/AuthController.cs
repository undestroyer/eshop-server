using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using server.Models.Data;
using server.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace server.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserManager<User> userManager, ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            // для такого проекта будем считать что username = phone.
            // по-хорошему нужно переопределить стандартный UserManager
            var user = await userManager.FindByNameAsync(loginModel.Phone).ConfigureAwait(false);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var token = GenerateToken(user);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var existingUser = await userManager.FindByNameAsync(registerModel.Phone).ConfigureAwait(false);
            if (existingUser != null)
            {
                ModelState.AddModelError(nameof(registerModel.Phone), "Пользователь с таким номером телефона уже существует");
                return UnprocessableEntity(ModelState);
            }

            var user = new User
            {
                UserName = registerModel.Phone,
                PhoneNumber = registerModel.Phone,
            };
            var result = await userManager.CreateAsync(user, registerModel.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                logger.LogError(result.ToString());
                throw new Exception("Не удалось сохранить данные пользователя");
            }

            var token = GenerateToken(user);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private SecurityToken GenerateToken(User user)
        {
            var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ItIsSimpleSecureKey"));

            return new JwtSecurityToken(
                issuer: "http://example.com",
                audience: "http://example.com",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}