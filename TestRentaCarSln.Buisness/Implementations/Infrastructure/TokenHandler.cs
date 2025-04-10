using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;

namespace TestRentaCar.Buisness.Implementations.Infrastructure
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenHandler(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateAccessToken(AppUser user)
        {
            TokenDto tokenDto = new();

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //Role verirem tokene
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //token konfigurasiyasi veririk
            tokenDto.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Token:AccessTokenExpirationInMinutes"]));
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issure"],
                expires: tokenDto.Expiration, //life time
                notBefore: DateTime.UtcNow, //islemeye baslayacagi vaxt
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new();
            //todo burda creat mehodun isledib sonra bunu isletmisik jwtAutoAndLogger dersimizde
            tokenDto.AccessToken = tokenHandler.WriteToken(securityToken);

            tokenDto.RefreshToken = CreateRefreshToken();

            return tokenDto;
        }

        public string CreateRefreshToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Token:RefreshTokenSecret"]); // Refresh token için gizli sifre
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(refreshToken);

        }
    }
}
