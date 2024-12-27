using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OneAuth.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController:ControllerBase
{
    #region AES
    public const string KEY = "oi2q3jrfou09q23hfoqiuhwciquhefc08qhf";
    [HttpPost("aes-enc")]
    public IActionResult AES_Encryption()
    {
        var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        var sign = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(sign);
        
        var payload = new JwtPayload();
        payload.AddClaim(new(ClaimTypes.Name, "张三"));
        payload.AddClaim(new(ClaimTypes.Email, "zhang.san@qq.com"));
        payload.AddClaim(new(ClaimTypes.Role, "管理员"));
        //payload.AddClaim(new(ClaimTypes.Role, "市场部"));
        payload.AddClaim(new(ClaimTypes.Role, "销售部"));
        payload.AddClaim(new("EmployeeNo", "5"));

        payload.AddClaim(new("Age", "25"));
        //payload.AddClaim(new("Age", "10"));

        var handler = new JwtSecurityTokenHandler();

        var token = new JwtSecurityToken(issuer: "STS",
                                         audience: "test",
                                         payload.Claims,
                                         notBefore: DateTime.Now,
                                         expires: DateTime.Now.AddMinutes(10),
                                         signingCredentials: header.SigningCredentials);
        
        var jwt = handler.WriteToken(token);

        return Ok(jwt);
    }

    [HttpPost("aes-des")]
    public IActionResult AES_Descryption([FromHeader(Name ="Authorization")]string? token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, new()
        {
            IssuerSigningKey = securityKey,
            RequireExpirationTime = false,
            ValidateAudience = false,
            ValidateIssuer = false
        }, out var jwtToken);



        return Ok(principal.Identity.Name);
    }
    #endregion

    #region RSA
    [HttpGet("rsa-generate")]
    public IActionResult RSA_Generate()
    {
        //.pem
        var rsa = RSA.Create();
        return Ok(new
        {
            publicKeyXml = rsa.ToXmlString(false),
            privateKeyXml = rsa.ToXmlString(true),
            publicKeyStr = Convert.ToBase64String(rsa.ExportRSAPublicKey()),
            privateKeyStr = Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
            publicKeyPem = rsa.ExportRSAPublicKeyPem(),
            privateKeyPem = rsa.ExportRSAPrivateKeyPem()
        });
    }

    public const string PUBLIC_KEY = "MIIBCgKCAQEA3WNoZ4Snw60yL2Kse0HPEfx3Z2GO7K9CQc6gTLSO1wOBA4oePR0R5tniCKl/xbyMXFiHKQtb86l4Wf4pQNYxdfyXLU10hddgnV7vupAG8zZ8S6pTvLDW9Y/DDQUQAumug1GICRbXTqZCS0PdTpcczEQXWtCR77vssuvBkZs5nBgdUYTxyWm+YhrkQyfVAIpNKCjO1YppNK8/mQVufju5WiJVNEpgFNozkCQnhPybZsd8rwGHHdqylNPohVNxWhqRQ3DuzuLhEZPoHXgHjBLobbfP4/O8GWBUIgnDJ/VDHkbqCoLX6no1ZntkBdO2G4TvUlBs8FCWDv/GuChFBW6q2QIDAQAB";
    const string PRIVATE_KEY = "MIIEowIBAAKCAQEA3WNoZ4Snw60yL2Kse0HPEfx3Z2GO7K9CQc6gTLSO1wOBA4oePR0R5tniCKl/xbyMXFiHKQtb86l4Wf4pQNYxdfyXLU10hddgnV7vupAG8zZ8S6pTvLDW9Y/DDQUQAumug1GICRbXTqZCS0PdTpcczEQXWtCR77vssuvBkZs5nBgdUYTxyWm+YhrkQyfVAIpNKCjO1YppNK8/mQVufju5WiJVNEpgFNozkCQnhPybZsd8rwGHHdqylNPohVNxWhqRQ3DuzuLhEZPoHXgHjBLobbfP4/O8GWBUIgnDJ/VDHkbqCoLX6no1ZntkBdO2G4TvUlBs8FCWDv/GuChFBW6q2QIDAQABAoIBAF0Q0uzhaRzrC/O7iUJvUbr/5LoC2vBIZJQBZoWYSYu9n1h7kWajd8kRwuFdT6cyMdcIKBlq+wadMUizfWgSIsy0mGCk9Nzmw2ikZiaJMULAntO4EGd794FXI+mvPSHcVk5B0evwPCaF4cx+BORqxeJgpWi50P90gnZcgQzSsg8Zk6GN2b7wIJRKMOohtcfNMbBfibcRS4/f1YQCD4ebIqcHdkEfuMfv1i/eiXgbWzjOmTHi6A7kkhpJwkmniezcbWR4WmEXvKgL16GhScXFwp7/QW+agWnZNmiNmiKZLmwZFFFuqINxdlf8KMFUPkd0JdvMa5EPo6b3RJ2MqHmKarkCgYEA4nS+0QLCI13a5zmvkyKa5BUIFg7Po3k3qF6IQ8dD59niqXtt8+qTRmVWZRm9mHVafEMWeD2RsJaC08vmPWbxr5/eLft2fzj0mpMvYvlqfPi1DHpGnvirrVADl1bFoXMb0knTQuf/b3MyXjwDhqxHgCLYad1MKAl+TUKOskLzJVMCgYEA+kVorHLu5lXUCgGChEYEbyMi27DT6UX7c+HmrLv2CCf4xypy9PySggyr9ekJRYxKi9GOJ75v6gx6tGNmqkXgI8XAl9C1ftvCJtCxBLM42p8y0YwrXd9bbPNmzbj1V2S89TMVEmqNalWcT5w8KgxBavLczRKnKqqCDfDqAZ0enaMCgYBCpcljCkTI096RHtElpJGhfu8aTtGdcxeGXgwMpqC9o8qpxsHdf7RIi5qZtrGuJRu0Zqo+SSCPsDxgySnB+II5BfwF3HRLjR+wPpE7t9w3W5stSO9v6g/cuifUap1PnukZQtECZ/Utf+HbCh6fjCSe+aJnxdGcFu8tHeGRZVLaMQKBgCI5T6EVbnntDrv3jOONt5mDEoc8XR4vRb2JDMReR7zBypFhyqqZx2clWHaeUXhDQQZxt6DTt/YnkrgMR8PNHmOF7VpVXhxk7N3l9+8Omx26W3awVlXV72isuEg1nMdArV5Sh/UmwQPjhSaV1NxjJKb9YxhgDqAVBQfnA/kIhWFFAoGBALBtut+fmnLK82yW/6lEaBOwVBbVB+Bl0r4/W4DF1P3P9kJYNuiRDiiLOkYgoRUcaBeIk0J+Qj803SdG8bsL44FwbSvB3VrcmJuUIFvUxSa5lja3s6hdAtSgjbv8elnGtWsble5LVz7ABwMAcARIy2LeutW/oCLcNzkT7CnKoge4";

    [HttpPost("rsa-enc")]
    public IActionResult RSA_Encryption()
    {
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(PRIVATE_KEY),out _);

        var securityKey = new RsaSecurityKey(rsa);
        var sign = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512);
        var header = new JwtHeader(sign);

        var payload = new JwtPayload();
        payload.AddClaim(new(ClaimTypes.Name, "张三"));
        payload.AddClaim(new(ClaimTypes.Email, "zhang.san@qq.com"));

        var handler = new JwtSecurityTokenHandler();
        var token = new JwtSecurityToken(header, payload);
        var jwt = handler.WriteToken(token);

        return Ok(jwt);
    }

    [HttpPost("rsa-des")]
    public IActionResult RSA_Descryption([FromHeader(Name = "Authorization")] string? token)
    {
        var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(PUBLIC_KEY),out _);
        var securityKey = new RsaSecurityKey(rsa);

        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, new()
        {
            IssuerSigningKey = securityKey,
            RequireExpirationTime = false,
            ValidateAudience = false,
            ValidateIssuer = false
        }, out var jwtToken);



        return Ok(principal.Identity.Name);
    }
    #endregion
}
