using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Yi.Framework.Rbac.Domain.Shared.Enums;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Blazor.Shared.Models;

namespace Blazor.Shared.Core.NewtonsoftJson;


public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _bearerToken;
    private readonly string VITE_APP_BASE_URL;

    public ApiClient(string bearerToken)
    {
        _httpClient = new HttpClient();
        _bearerToken = bearerToken;
        //VITE_APP_BASE_URL = AppSettings.app(new string[] { "API", "VITE_APP_BASE_URL" }).ToString();
        VITE_APP_BASE_URL = "http://localhost:19001/api/app/";
    }

    public ApiClient()
    {
        _httpClient = new HttpClient();
        //VITE_APP_BASE_URL = AppSettings.app(new string[] { "API", "VITE_APP_BASE_URL" }).ToString();

            VITE_APP_BASE_URL = "http://localhost:19001/api/app/";
    }



    public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default)  
    {
        string responseBody = "";
        try
        {
            string requestUri = VITE_APP_BASE_URL + url;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken); // 设置请求头，添加 Bearer Token                                                                                                                     // 
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);// 发送 GET 请求      
            response.EnsureSuccessStatusCode();// 确保响应是成功的        
             responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);  // 读取响应内容       
            T? result= JsonConvert.DeserializeObject<T>(responseBody);
            return result;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return default(T);
    }

    public async Task<TResult> PostAsync<TResult, TEntity>(string url, TEntity entity)
    {
        string responseBody = "";
        try
        {
           
            string requestUri = VITE_APP_BASE_URL + url;
            var json = JsonConvert.SerializeObject(entity);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(requestUri, content);
            var response = await _httpClient.PostAsJsonAsync(requestUri, entity);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);  // 读取响应内容    
            TResult? result = JsonConvert.DeserializeObject<TResult>(responseBody);
            return result;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return default(TResult);
    }



}

