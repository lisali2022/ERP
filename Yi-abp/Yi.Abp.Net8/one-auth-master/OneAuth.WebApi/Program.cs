using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;

using OneAuth.WebApi.Controllers;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using OneAuth.WebApi.Requirements;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen(configure =>
{
    configure.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "JWT",
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
    });
    configure.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new ()
            { 
                Reference=new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT",
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication()
    //.AddCookie()
    .AddJwtBearer(options =>
    {
        #region AES
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthController.KEY)),
            RequireExpirationTime = false,
            ValidateAudience = false,
            ValidateIssuer = false,
            //ValidAudience ="test",
            //ValidIssuer="STS",
            //LifetimeValidator= (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
            //{
            //    if(!expires.HasValue || expires.Value> securityToken.ValidFrom)
            //    {
            //        return false;
            //    }

            //    return true;
            //}
        };
        #endregion

        #region RSA

        //var rsa = RSA.Create();
        //rsa.ImportRSAPublicKey(Convert.FromBase64String(AuthController.PUBLIC_KEY), out _);
        //var securityKey = new RsaSecurityKey(rsa);

        //options.TokenValidationParameters = new TokenValidationParameters()
        //{
        //    IssuerSigningKey = securityKey,
        //    RequireExpirationTime = false,
        //    ValidateAudience = false,
        //    ValidateIssuer = false
        //};


        #endregion
    })
    ;

builder.Services.AddSingleton<IAuthorizationHandler, AboveAgeRequirementHandler>();

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("HasEmailPolicy",policy=>policy.RequireClaim(ClaimTypes.Email));

    builder.AddPolicy("HasEmployeNo", policy => policy.RequireClaim("EmployeeNo", "1", "2", "3", "4", "5"));

    builder.AddPolicy("Above18", policy => policy.Requirements.Add(new AboveAgeRequirement(18)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
