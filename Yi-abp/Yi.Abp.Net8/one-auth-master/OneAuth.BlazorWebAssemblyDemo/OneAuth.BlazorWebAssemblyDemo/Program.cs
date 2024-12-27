using System.Text;

using Microsoft.IdentityModel.Tokens;

using OneAuth.BlazorWebAssemblyDemo.Client.Pages;
using OneAuth.BlazorWebAssemblyDemo.Components;
using OneAuth.BlazorWebAssemblyDemo.Controllers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OneAuth.BlazorWebAssemblyDemo.Client;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthController.KEY)),
            ValidateActor = false,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
        };    
    })
    ;

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Above21", policy =>
    {
        policy.RequireClaim(ClaimTypes.DateOfBirth)
        .RequireAssertion(context =>
        {
            var birthDay = context.User.FindFirstValue(ClaimTypes.DateOfBirth);
            return (DateOnly.TryParse(birthDay, out var birth) && DateTime.Now.Date.Year - birth.Year > 21);
        });
    });
});

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();

builder.Services.AddCors();  //2024-10-03

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(OneAuth.BlazorWebAssemblyDemo.Client._Imports).Assembly);  //添加附加程序集

app.Run();
