using Blazor.Shared.Core.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Threading.Tasks;
using Yi.Framework.Rbac.Domain.Shared.Dtos;

namespace Blazor.Shared.Models
{
    public static class AppsettingsUtility
    {
        private static ConnectionStrings _conn;
        private static DD _dd;
        public static string VITE_APP_BASE_URL; 
        public static ConnectionStrings Conn { get { return _conn; } }
        public static DD dd { get { return _dd; } }
        public static UserRoleMenuDto? UserRoleMenuDto { get; set; }
        public static string? Token { get; set; }
        public static ApiClient? ApiClient { get; set; }

        /// <summary>
        /// 将配置项的值赋值给属性
        /// </summary>
        /// <param name="configuration"></param>
        public  static void InitialConfig(IConfiguration configuration)
        {
            VITE_APP_BASE_URL= AppSettings.app(new string[] { "API", "VITE_APP_BASE_URL" }).ToString();
            //VITE_APP_BASE_URL = configuration["API:VITE_APP_BASE_URL"];
            ConnectionStrings conn = new ConnectionStrings();
            conn.FM = configuration["ConnectionStrings:FM"];
            conn.FMTTEST = configuration["ConnectionStrings:FMTTEST"];
            conn.ZT = configuration["ConnectionStrings:ZT"];
            conn.ZC = configuration["ConnectionStrings:ZC"];
            _conn = conn;

            DD dd = new DD();
            dd.DDAgentId = Convert.ToInt64(configuration["DD:DDAgentId"]);
            dd.DDCorpId = configuration["DD:DDCorpId"];
            dd.DDAppKey = configuration["DD:DDAppKey"];
            dd.DDAppSecret = configuration["DD:DDAppSecret"];
            dd.DDCorpsecret = configuration["DD:DDCorpsecret"];
            _dd = dd;
        }

        public async static Task InitialApi(string token)
        {
            Token = token;
            ApiClient = ApiClient ?? new ApiClient(token);
             UserRoleMenuDto = await ApiClient.GetAsync<UserRoleMenuDto>("account");
        }
    }
}
