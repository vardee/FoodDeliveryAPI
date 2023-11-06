using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

public class TokenHelper
{
    private readonly IConfiguration _configuration;

    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
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
}
