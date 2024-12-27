using Blazored.LocalStorage;
using BootstrapBlazor.OnlyServer.Components;
using BootstrapBlazor.OnlyServer.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text;
using Blazor.Shared.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar.Extensions;
using BootstrapBlazor.OnlyServer.Services;
using Telerik.SvgIcons;
using BootstrapBlazor.Components;
using Microsoft.Extensions.Options;
using Blazor.Shared.Services;
using Telerik.Blazor.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddSingleton(new AppSettings(builder.Configuration)); //配置文件 //https://www.cnblogs.com/handsomeziff/p/17973938
AppsettingsUtility.InitialConfig(builder.Configuration); //初始化全局变量 2024-12-23

#region BootstrapBlazor
builder.Services.AddBootstrapBlazor();
//2024-12-24 jammie 增加BootstrapBlazor(多语言支持配置信息 https://localhost:5053/localization
builder.Services.AddRequestLocalization<IOptionsMonitor<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
{
    blazorOption.OnChange(op => Invoke(op));
    Invoke(blazorOption.CurrentValue);
    return;
    void Invoke(BootstrapBlazorOptions option)
    {
        var supportedCultures = option.GetSupportedCultures();
        localizerOption.SupportedCultures = supportedCultures;
        localizerOption.SupportedUICultures = supportedCultures;
    }
});
#endregion 


#region TelerikBlazor
builder.Services.AddTelerikBlazor(); //2024-12-20 lisa add
builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(ResxLocalizer)); //注入TK本地化
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new List<CultureInfo>()
//                {
//                    new CultureInfo("zh-Hans"),
//                    new CultureInfo("en-US"),
//                    new CultureInfo("de-DE"),
//                    new CultureInfo("es-ES"),
//                    new CultureInfo("bg-BG"),
//                };

//    options.DefaultRequestCulture = new RequestCulture("zh-Hans");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});
#endregion

builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddTableDemoDataService();// 增加 Table 数据服务操作类 报错,先注释


//https://www.cnblogs.com/HTLucky/p/13194830.html
//全局配置Json序列化处理
builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用          
    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //不使用驼峰样式的key               
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd"; //设置时间格式
});

//Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddRazorComponents().AddInteractiveServerComponents(); 

#region OneAuth
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

// 增加 SignalR 服务数据传输大小限制配置
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);
var app = builder.Build();

//2024-12-24 jammie  BootstrapBlazor  启用本地化  https://localhost:5053/localization
var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (option != null)
{
    app.UseRequestLocalization(option.Value);
}

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
