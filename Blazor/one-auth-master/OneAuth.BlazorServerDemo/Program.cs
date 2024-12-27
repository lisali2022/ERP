using System.Security.Claims;
using Blazored.LocalStorage;
using OneAuth.BlazorServerDemo.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient(); // ���ע�� IHttpClientFactory ��Ĭ�ϵ� HttpClient
builder.Services.AddControllers();
builder.Services.AddBlazoredLocalStorage();  //���س־û�

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    ;

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/api/auth/sign-out";
        options.AccessDeniedPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Above21", policy =>
    {
        policy.RequireClaim(ClaimTypes.DateOfBirth)
        .RequireAssertion(context =>
        {
            var birthDay = context.User.FindFirstValue(ClaimTypes.DateOfBirth);
           return  (DateOnly.TryParse(birthDay, out var birth) && DateTime.Now.Date.Year - birth.Year > 21) ;
        });
    });
});

builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
