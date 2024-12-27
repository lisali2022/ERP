using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Yi.Framework.Rbac.Domain.Shared.Enums;
namespace Blazor.Shared.Core.TextJson;
public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _bearerToken;

    //序列化总是报错,改为用 Newtonsoft.Json
    public ApiClient(string bearerToken)
    {
        _httpClient = new HttpClient();
        _bearerToken = bearerToken;
    }

    public async Task<T?> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default)  
    {
        string responseBody = "";
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken); // 设置请求头，添加 Bearer Token         
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);// 发送 GET 请求                                                                                                                       // 
            response.EnsureSuccessStatusCode();// 确保响应是成功的        
             responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);  // 读取响应内容
                                                                                               // 
            //T result = JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });  // 将 JSON 响应反序列化为 T 类型的对象  

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {
                    new SexEnumConverter()
                }
            };
            T result=JsonSerializer.Deserialize<T>(responseBody, options);
            return result;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return default(T);      
    }

    //解决枚举值不能报错的问题
    public class SexEnumConverter : JsonConverter<SexEnum>
    {
        public override SexEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "Male" => SexEnum.Male,
                "Female" => SexEnum.Woman,
                _ => throw new JsonException($"Unknown SexEnum value: {reader.GetString()}")
            };
        }

        public override void Write(Utf8JsonWriter writer, SexEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}