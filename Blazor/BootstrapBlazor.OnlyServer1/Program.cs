using Blazor.Shared.Models;
using Blazored.LocalStorage;
using BootstrapBlazor.OnlyServer1.Components;
using BootstrapBlazor.OnlyServer1.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new AppSettings(builder.Configuration)); //配置文件 //https://www.cnblogs.com/handsomeziff/p/17973938
AppsettingsUtility.InitialConfig(builder.Configuration); //初始化全局变量 2024-12-23

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddBootstrapBlazor();

builder.Services.AddSingleton<WeatherForecastService>();

// 增加 Table 数据服务操作类
builder.Services.AddTableDemoDataService();

// 增加 SignalR 服务数据传输大小限制配置
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);



#region OneAuth  2024-12-25
builder.Services.AddHttpClient(); // 这会注册 IHttpClientFactory 和默认的 HttpClientOneAuth
builder.Services.AddControllers(); //
builder.Services.AddControllersWithViews();  //2024-10-15
builder.Services.AddBlazoredLocalStorage();  //本地持久化 
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseResponseCompression();
}

app.UseStaticFiles();

app.UseAntiforgery();
app.MapControllers();  //OneAuth
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
