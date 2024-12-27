using Blazor.Shared.Core.NewtonsoftJson;
using Blazor.Shared.Models;
using BootstrapBlazor.Components;
using BootstrapBlazor.OnlyServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using SqlSugar;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using Telerik.Blazor.Components.Common.Trees.Models;
using Telerik.SvgIcons;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Menu;

namespace BootstrapBlazor.OnlyServer.Components.Pages.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Menu
    {
        [NotNull]
        private List<MenuGetListOutputDto>? TreeItems { get; set; }
        //路由中的AuthorizeRouteView组件参数
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<MenuGetListOutputDto>? Localizer { get; set; }

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
        private IEnumerable<MenuGetListOutputDto>? Items { get; set; }
        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<MenuGetListOutputDto>, string, SortOrder, IEnumerable<MenuGetListOutputDto>>> SortLambdaCache = new();
        private static Task<MenuGetListOutputDto> OnAddAsync() => Task.FromResult(new MenuGetListOutputDto() { CreationTime = DateTime.Now });

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var treeItems= await AppsettingsUtility.ApiClient.GetAsync<PagedResultDto<MenuGetListOutputDto>>("menu?visible=true");   // 从数据库中获得
            TreeItems = (List<MenuGetListOutputDto>)treeItems.Items;
            TreeItems = TreeItems.OrderBy(x => x.OrderNum).ToList(); //排序
        }

        private Task<bool> OnSaveAsync(MenuGetListOutputDto item, ItemChangedType changedType)
        {
            if (changedType == ItemChangedType.Add)
            {
                item.Id =Guid.NewGuid();
                TreeItems.Add(item);
            }
            else
            {
                var oldItem = TreeItems.FirstOrDefault(i => i.Id == item.Id);
                if (oldItem != null)
                {
                    oldItem.MenuName = item.MenuName;
                    oldItem.OrderNum = item.OrderNum;
                    oldItem.Url = item.Url;
                    oldItem.Component = item.Component;
                }
            }
            return Task.FromResult(true);
        }

        private Task<bool> OnDeleteAsync(IEnumerable<MenuGetListOutputDto> items)
        {
            TreeItems.RemoveAll(menu => items.Any(i => i.Id == menu.Id));
            return Task.FromResult(true);
        }


        //第二步：设置 Items 或者 OnQueryAsync 获得组件数据集合
        private async Task<QueryData<MenuGetListOutputDto>> OnQueryAsync(QueryPageOptions options)
        {
            // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
            if (Items == null || !Items.Any())
            {               
                //var state = await StateTask;
                //var user = state.User;
                //string token = user.FindFirstValue(ClaimTypes.Authentication);
                var PagedResultDto = await AppsettingsUtility.ApiClient.GetAsync<PagedResultDto<MenuGetListOutputDto>>("menu?visible=true");             
                Items = PagedResultDto.Items.OrderBy(x => x.OrderNum).ToList();
            }

            

            var items = Items;
            var isSearched = false;
            // 处理高级查询
            if (options.SearchModel is MenuGetListOutputDto model)
            {
                if (!string.IsNullOrEmpty(model.MenuName))
                {
                    items = items.Where(item => item.MenuName?.Contains(model.MenuName, StringComparison.OrdinalIgnoreCase) ?? false);
                }     
                isSearched = !string.IsNullOrEmpty(model.MenuName) || !string.IsNullOrEmpty(model.MenuName);
            }

            if (options.Searches.Any())
            {
                // 针对 SearchText 进行模糊查询
                items = items.Where(options.Searches.GetFilterFunc<MenuGetListOutputDto>(FilterLogic.Or));
            }

            // 过滤
            var isFiltered = false;
            if (options.Filters.Any())
            {
                items = items.Where(options.Filters.GetFilterFunc<MenuGetListOutputDto>());
                isFiltered = true;
            }

            // 排序
            var isSorted = false;
            if (!string.IsNullOrEmpty(options.SortName))
            {
                // 外部未进行排序，内部自动进行排序处理
                var invoker = SortLambdaCache.GetOrAdd(typeof(MenuGetListOutputDto), key => LambdaExtensions.GetSortLambda<MenuGetListOutputDto>().Compile());
                items = invoker(items, options.SortName, options.SortOrder);
                isSorted = true;
            }
            var total = items.Count();
            var lst = new QueryData<MenuGetListOutputDto>()
            {
                Items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
                TotalCount = total,
                IsFiltered = isFiltered,
                IsSorted = isSorted,
                IsSearch = isSearched
            };
            return await Task.FromResult(lst);
        }


        //第三步：设置 TreeNodeConverter 将组件数据集合转化为树状结构
        private static Task<IEnumerable<TableTreeNode<MenuGetListOutputDto>>> TreeNodeConverter(IEnumerable<MenuGetListOutputDto> items)
        {
            //Task.Delay(3000);
            var ret = BuildTreeNodes(items, Guid.Empty);// 构造树状数据结构
            return Task.FromResult(ret);

            IEnumerable<TableTreeNode<MenuGetListOutputDto>> BuildTreeNodes(IEnumerable<MenuGetListOutputDto> items, Guid parentId)
            {
                var lst = items.Where(i => i.ParentId == parentId);
                var lstChildren = lst.Select((menu) => new TableTreeNode<MenuGetListOutputDto>(menu)
                {
                    HasChildren = items.Any(i => i.ParentId == menu.Id),// 此处为示例，假设偶行数据都有子数据  设置 是否有子节点 默认 false 用于判断是否有子节点               
                    IsExpand = items.Any(i => i.ParentId == menu.Id), // 如果子项集合有值 则默认展开此节点  设置 是否展开 默认 false              
                    Items = BuildTreeNodes(items.Where(i => i.ParentId == menu.Id), menu.Id)// 获得子项集合 设置 子节点集合
                });

                var ret = new List<TableTreeNode<MenuGetListOutputDto>>();
                ret.AddRange(lstChildren); //添加子节点 AddRange  来一次性向集合中添加多个元素
                return ret;
            }
        }

        //设置 OnTreeExpand 回调委托响应行展开获取子项数据集合
        private Task<IEnumerable<TableTreeNode<MenuGetListOutputDto>>> OnTreeExpand(MenuGetListOutputDto menu)
        {
            IEnumerable<TableTreeNode<MenuGetListOutputDto>> lst = null;
            if (menu != null)
            {
                return Task.FromResult(lst);
            }
            else
            {
                return null;
            }            
        }
    }
}
