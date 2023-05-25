using Courseproject.Common.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Courseproject.API.CQRS.IdentityHandler;

public class GenerateToken
{
    private readonly IConfiguration _config;
    public GenerateToken(IConfiguration config)
    {
        _config = config;
    }
    public string generateTokens(User user) 
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.email),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
        SigningCredentials signingcred = new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha512Signature);
        var securityToken = new JwtSecurityToken(
            claims:claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer : _config.GetSection("Jwt:Issuer").Value,
            audience : _config.GetSection("Jwt:Audience").Value,
            signingCredentials : signingcred
            
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }
}
