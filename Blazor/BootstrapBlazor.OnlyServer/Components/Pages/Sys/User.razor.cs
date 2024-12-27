using Blazor.Shared.Core.NewtonsoftJson;
using Blazor.Shared.Models;
using BootstrapBlazor.Components;
using BootstrapBlazor.OnlyServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Application.Contracts.Dtos.User;
namespace BootstrapBlazor.OnlyServer.Components.Pages.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public partial class User
    {

        //路由中的AuthorizeRouteView组件参数
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<UserGetListOutputDto>? Localizer { get; set; }

        /// <summary>
        /// 获得/设置 分页配置数据源
        /// </summary>
        private static IEnumerable<int> PageItemsSource => new int[] { 10, 20, 40 };

        private static string GetAvatarUrl(int id) => $"images/avatars/150-{id}.jpg";

        private static Color GetProgressColor(int count) => count switch
        {
            >= 0 and < 10 => Color.Secondary,
            >= 10 and < 20 => Color.Danger,
            >= 20 and < 40 => Color.Warning,
            >= 40 and < 50 => Color.Info,
            >= 50 and < 70 => Color.Primary,
            _ => Color.Success
        };

        [NotNull]
        private IEnumerable<UserGetListOutputDto>? Items { get; set; }

        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<UserGetListOutputDto>, string, SortOrder, IEnumerable<UserGetListOutputDto>>> SortLambdaCache = new();

        private async Task<QueryData<UserGetListOutputDto>> OnQueryAsync(QueryPageOptions options)
        {
            // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
            if (Items == null || !Items.Any())
            {
                var state = await StateTask;
                var user = state.User;
                string token = user.FindFirstValue(ClaimTypes.Authentication);
                string VITE_APP_BASE_URL = AppSettings.app(new string[] { "API", "VITE_APP_BASE_URL" }).ToString();
                string url = VITE_APP_BASE_URL + "User?visible=true";
                ApiClient apiClient = new ApiClient(token);
                var PagedResultDto = await apiClient.GetAsync<PagedResultDto<UserGetListOutputDto>>(url);
                Items = PagedResultDto.Items.ToList();
            }

            var items = Items;
            var isSearched = false;
            // 处理高级查询
            if (options.SearchModel is UserGetListOutputDto model)
            {
                if (!string.IsNullOrEmpty(model.UserName))
                {
                    items = items.Where(item => item.UserName?.Contains(model.UserName, StringComparison.OrdinalIgnoreCase) ?? false);
                }
                isSearched = !string.IsNullOrEmpty(model.UserName) || !string.IsNullOrEmpty(model.UserName);
            }

            if (options.Searches.Any())
            {
                // 针对 SearchText 进行模糊查询
                items = items.Where(options.Searches.GetFilterFunc<UserGetListOutputDto>(FilterLogic.Or));
            }

            // 过滤
            var isFiltered = false;
            if (options.Filters.Any())
            {
                items = items.Where(options.Filters.GetFilterFunc<UserGetListOutputDto>());
                isFiltered = true;
            }

            // 排序
            var isSorted = false;
            if (!string.IsNullOrEmpty(options.SortName))
            {
                // 外部未进行排序，内部自动进行排序处理
                var invoker = SortLambdaCache.GetOrAdd(typeof(UserGetListOutputDto), key => LambdaExtensions.GetSortLambda<UserGetListOutputDto>().Compile());
                items = invoker(items, options.SortName, options.SortOrder);
                isSorted = true;
            }
            var total = items.Count();
            return await Task.FromResult(new QueryData<UserGetListOutputDto>()
            {
                Items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
                TotalCount = total,
                IsFiltered = isFiltered,
                IsSorted = isSorted,
                IsSearch = isSearched
            });
        }
    }
}
