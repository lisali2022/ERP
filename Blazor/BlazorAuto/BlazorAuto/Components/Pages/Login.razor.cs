using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Blazor.Shared.Models;
using System.Text.Json;
using Yi.Framework.Rbac.Domain.Shared.Dtos;

namespace BlazorAuto.Components.Pages
{
    public partial class Login
    {
        [SupplyParameterFromQuery]
        [Parameter] public string? ReturnUrl { get; set; }
        string msg { get; set; } = "";

        UserDto Model { get; set; } = new UserDto
        {
            UserName = "cc",
            Password = "123456"
        };

        async Task SubmitAsync()
        {
            string accessToken = await GetDingTalkAccessTokenAsync();
            string jsonString = JsonSerializer.Serialize(Model);
            string url = "api/auth";
            url = "http://localhost:19001/api/app/account/login";
            try
            {
                var response = await Client.PostAsJsonAsync(url, Model);

                // HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var tokenStr = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(tokenStr))
                {
                    JObject jsonObject = JObject.Parse(tokenStr);
                    if (jsonObject.ContainsKey("token"))
                    {
                        string token = jsonObject["token"].ToString();
                        await LocalStorageService.SetItemAsStringAsync("TOKEN", token);
                        msg = token;
                        Navigator.NavigateTo("/");
                    }
                    else
                    {
                        msg = "帐户或者密码不正确!";

                    }
                }
            }
            catch (HttpRequestException e)
            {
                // Console.WriteLine("\nException Caught!");
                // Console.WriteLine();
                msg = e.Message;
            }
        }
        private async Task<string> GetDingTalkAccessTokenAsync()
        {
            var appId = "dingk1ijv6gjwuupzpft"; // 替换为你的AppKey
            var appSecret = "X_PIpWffCW1O7QmfaeLduwf2tdWVCrYRX93AUwYshzj9yez0bbeZQZEawtsyLOF8"; // 替换为你的AppSecret
            try
            {
                var response = await Client.GetAsync("https://oapi.dingtalk.com/gettoken?appkey=" + appId + "&appsecret=" + appSecret);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                // 这里需要解析responseBody来获取access_token，具体取决于钉钉API的返回格式
                // 假设返回的是一个JSON对象，你可以使用System.Text.Json来反序列化它
                // 下面是一个简化的示例，你需要根据实际情况进行调整
                // var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(responseBody);
                // return tokenResponse?.access_token;

                // 由于这是一个示例，我们直接返回一个假设的access_token
                return responseBody.ToString(); // 替换为实际的access_token
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "";
            }

        }
    }
}
