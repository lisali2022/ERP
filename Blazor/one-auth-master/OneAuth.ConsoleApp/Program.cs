
//WindowsPrincial 和 WindowsIdentity

using System.Security.Claims;
using System.Security.Principal;

//var name = WindowsIdentity.GetCurrent().Name;
//Console.WriteLine("Windows 用户名：{0}",name);







//GenaricPrincipal 和 GenaricIdentity

//GenericIdentity genericIdentity = new("张三");
//GenericPrincipal genericPrincipal = new(genericIdentity, ["Admin"]);

//Console.WriteLine(genericPrincipal.Identity.Name);




//ClaimsPrincipal 和 ClaimsIdentity


ClaimsIdentity claimsIdentity = new(authenticationType:"QQ");

Claim name = new(ClaimTypes.Name,"张三");
Claim email = new(ClaimTypes.Email, "zhangsan@qq.com");
Claim phone = new(ClaimTypes.MobilePhone, "123456");

claimsIdentity.AddClaims([name, email, phone]);

ClaimsIdentity wechat = new("微信");
wechat.AddClaim(new(ClaimTypes.Name, "周老师"));

ClaimsPrincipal principal = new([claimsIdentity, wechat]);

//Console.WriteLine("用户名：{0}",principal.Identity.Name);
//Console.WriteLine("认证成功：{0}",principal.Identity.IsAuthenticated);
//Console.WriteLine("认证类型：{0}",principal.Identity.AuthenticationType);


foreach (var item in principal.Identities)
{
    Console.WriteLine("认证类型：{0}", item.AuthenticationType);
    Console.WriteLine("用户名：{0}", item.Name);
    Console.WriteLine("认证成功：{0}", item.IsAuthenticated);

    foreach (var claim in item.Claims)
    {
        Console.WriteLine("\t{0}:{1}", claim.Type, claim.Value);
    }
}