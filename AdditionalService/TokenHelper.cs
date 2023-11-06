using backendTask.DataBase.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenHelper
{
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = configuration.GetValue<string>("AppSettings:Secret");
        _issuer = configuration.GetValue<string>("AppSettings:Issuer");
        _audience = configuration.GetValue<string>("AppSettings:Audience");
    }

    public string GetUserEmailFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        string email = "";

        if (jwtToken.Payload.TryGetValue("email", out var emailObj) && emailObj is string emailValue)
        {
            email = emailValue;
        }

        return email;
    }
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    private string GetAuthTokenFromRequest()
    {
        string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader.Substring("Bearer ".Length);
        }
        return null;
    }
}
