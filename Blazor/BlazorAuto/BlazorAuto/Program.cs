using BlazorAuto.Client.Pages;
using BlazorAuto.Components;
using Blazored.LocalStorage;
using System.Security.Claims;

namespace BlazorAuto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient(); // 这会注册 IHttpClientFactory 和默认的 HttpClientOneAuth
            builder.Services.AddControllers(); //OneAuth
            builder.Services.AddBlazoredLocalStorage();  //本地持久化 OneAuth


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();


            #region OneAuth

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
                        return (DateOnly.TryParse(birthDay, out var birth) && DateTime.Now.Date.Year - birth.Year > 21);
                    });
                });
            });

            builder.Services.AddCascadingAuthenticationState();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            app.MapControllers();  //OneAuth
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
