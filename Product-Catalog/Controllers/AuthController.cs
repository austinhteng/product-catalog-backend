using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login([FromBody] bool isAdmin)
    {
        var token = GenerateJwtToken(isAdmin);
        return Ok(new { token });
    }

    private string GenerateJwtToken(bool isAdmin)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "Username"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", (isAdmin) ? "admin" : "user"),
            new Claim("Country", "United States")
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