using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        var token = GenerateJwtToken("admin");
        return Ok(new { token });
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", "admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mY!8yvVonMgBmg>.p})BewBp_9ZQ+?nZTj,"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://product-catalog.com",
            audience: "http://product-catalog.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}