using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.OnlyServer.Controllers;

/// <summary>
/// BootstrapBlazor 文化 Controller  在组件CultureChooser的SetCulture方法中被调用 
/// BootstrapBlazor实现 UI 本地化信息存储（例如，cookie）
/// https://localhost:5053/localization
/// </summary>
[Route("[controller]/[action]")]
public class CultureController : Controller
{
    /// <summary>
    /// 设置文化方法
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public IActionResult SetCulture(string culture, string redirectUri)
    {
        if (string.IsNullOrEmpty(culture))
        {
            HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
        }
        else
        {
            //存储到Cookies
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddYears(1)
                });
        }

        return LocalRedirect(redirectUri);
    }

    /// <summary>
    /// 重置文化方法
    /// </summary>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public IActionResult ResetCulture(string redirectUri)
    {
        HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

        return LocalRedirect(redirectUri);
    }
}
