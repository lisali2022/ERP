﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ListView 组件基类
/// </summary>
public partial class ListView<TItem> : BootstrapComponentBase
{
    private string? ClassString => CssBuilder.Default("listview")
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BodyClassString => CssBuilder.Default("listview-body")
        .AddClass("is-group", GroupName != null)
        .Build();

    /// <summary>
    /// 获得/设置 CardHeard
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 排序回调方法 默认 null 使用内置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public IOrderedEnumerable<IGrouping<object?, TItem>>? GroupOrderCallback { get; set; }

    /// <summary>
    /// 获得/设置 BodyTemplate
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
#if NET6_0_OR_GREATER
    [Microsoft.AspNetCore.Components.EditorRequired]
#endif
  public Microsoft.AspNetCore.Components.RenderFragment<TItem>? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 FooterTemplate 默认 null 未设置 设置值后 <see cref="Pageable"/> 参数不起作用，请自行实现分页功能
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否分页 默认为 false 不分页 设置 <see cref="FooterTemplate"/> 时分页功能自动被禁用
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool Pageable { get; set; }

    /// <summary>
    /// 获得/设置 分组 Lambda 表达式 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<TItem, object?>? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 是否可折叠 默认 false 需要开启分组设置 <see cref="GroupName"/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool Collapsible { get; set; }

    /// <summary>
    /// 获得/设置 是否手风琴效果 默认 false 需要开启可收缩设置 <see cref="Collapsible"/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// 获得/设置 CollapseItem 展开收缩时回调方法 默认 false 需要开启可收缩设置 <see cref="Collapsible"/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<CollapseItem, Task>? OnCollapseChanged { get; set; }

    /// <summary>
    /// 获得/设置 首次渲染是否收缩回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<object?, bool>? CollapsedGroupCallback { get; set; }

    /// <summary>
    /// 异步查询回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// 获得/设置 ListView组件元素点击时回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<TItem, Task>? OnListViewItemClick { get; set; }

    /// <summary>
    /// 获得/设置 是否为竖向排列 默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 每页数据数量 默认 20
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int PageItems { get; set; } = 20;

    /// <summary>
    /// 获得/设置 当前页码
    /// </summary>
    private int PageIndex { get; set; }

    /// <summary>
    /// 获得/设置 数据总条目
    /// </summary>
    protected int TotalCount { get; set; }

    /// <summary>
    /// 数据集合内部使用
    /// </summary>
    protected IEnumerable<TItem> Rows => Items ?? Enumerable.Empty<TItem>();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        if (Items == null)
        {
            await QueryData();
        }
        if (GroupName != null)
        {
            GroupOrderCallback = Rows.GroupBy(GroupName).OrderBy(k => k.Key);
        }
    }

    private bool IsCollapsed(int index, object? groupKey) => CollapsedGroupCallback?.Invoke(groupKey) ?? index > 0;

    /// <summary>
    /// 点击页码调用此方法
    /// </summary>
    /// <param name="pageIndex"></param>
    protected Task OnPageLinkClick(int pageIndex) => QueryAsync(pageIndex);

    /// <summary>
    /// 查询按钮调用此方法
    /// </summary>
    /// <returns></returns>
    public async Task QueryAsync(int pageIndex = 1)
    {
        PageIndex = pageIndex;
        await QueryData();
        StateHasChanged();
    }

    /// <summary>
    /// 调用 OnQuery 回调方法获得数据源
    /// </summary>
    protected async Task QueryData()
    {
        QueryData<TItem>? queryData = null;
        if (OnQueryAsync != null)
        {
            queryData = await OnQueryAsync(new QueryPageOptions()
            {
                PageIndex = PageIndex,
                PageItems = PageItems,
            });
        }
        if (queryData != null)
        {
            Items = queryData.Items;
            TotalCount = queryData.TotalCount;
        }
    }

    private int PageCount => (int)Math.Ceiling(TotalCount * 1.0 / PageItems);

    /// <summary>
    /// 点击元素事件
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected async Task OnClick(TItem item)
    {
        if (OnListViewItemClick != null)
        {
            await OnListViewItemClick(item);
        }
    }

    private Microsoft.AspNetCore.Components.RenderFragment RenderCollapsibleItems() => builder =>
    {
        var index = 0;
        foreach (var key in GroupOrderCallback)
        {
            var i = index++;
            builder.AddContent(i, RenderItem(key, i));
        }
    };
}
