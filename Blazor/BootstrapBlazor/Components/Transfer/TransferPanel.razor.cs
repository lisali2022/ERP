﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TransferPanelBase 穿梭框面板组件
/// </summary>
public partial class TransferPanel
{
    /// <summary>
    /// 获得/设置 搜索关键字
    /// </summary>
    protected string? SearchText { get; set; }

    private string? PanelClassString => CssBuilder.Default("transfer-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 搜索图标样式
    /// </summary>
    private string? SearchClass => CssBuilder.Default("input-prefix")
        .AddClass("is-on", !string.IsNullOrEmpty(SearchText))
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 获得 Panel 样式
    /// </summary>
    private string? PanelListClassString => CssBuilder.Default("transfer-panel-list scroll")
        .AddClass("search", ShowSearch)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? GetItemClass(SelectedItem item) => CssBuilder.Default("transfer-panel-item")
        .AddClass(OnSetItemClass?.Invoke(item))
        .Build();

    /// <summary>
    /// 获得 组件是否被禁用属性值
    /// </summary>
    private string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// 获得/设置 数据集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
#if NET6_0_OR_GREATER
    [Microsoft.AspNetCore.Components.EditorRequired]
#endif
    public List<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 数据样式回调方法 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<SelectedItem, string?>? OnSetItemClass { get; set; }

    /// <summary>
    /// 获得/设置 面板显示文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 搜索框图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// 获得/设置 选项状态变化时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    /// 获得/设置 搜索框的 placeholder 字符串
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? SearchPlaceHolderString { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<List<SelectedItem>>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Item 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<SelectedItem>? ItemTemplate { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Transfer<string>>? Localizer { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        SearchPlaceHolderString ??= Localizer[nameof(SearchPlaceHolderString)];
        Text ??= Localizer[nameof(Text)];

        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.TransferPanelSearchIcon);
    }

    /// <summary>
    /// 头部复选框初始化值方法
    /// </summary>
    protected CheckboxState HeaderCheckState()
    {
        var ret = CheckboxState.Indeterminate;
        if (Items.Any() && Items.All(i => i.Active))
        {
            ret = CheckboxState.Checked;
        }
        else if (!Items.Any(i => i.Active))
        {
            ret = CheckboxState.UnChecked;
        }

        return ret;
    }

    /// <summary>
    /// 左侧头部复选框初始化值方法
    /// </summary>
    protected async Task OnHeaderCheck(CheckboxState state, SelectedItem item)
    {
        if (Items != null)
        {
            if (state == CheckboxState.Checked)
            {
                GetShownItems().ForEach(i => i.Active = true);
            }
            else
            {
                GetShownItems().ForEach(i => i.Active = false);
            }

            if (OnSelectedItemsChanged != null)
            {
                await OnSelectedItemsChanged();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected async Task OnStateChanged(CheckboxState state, SelectedItem item)
    {
        // trigger when transfer item clicked
        item.Active = state == CheckboxState.Checked;

        // set header
        if (OnSelectedItemsChanged != null)
        {
            await OnSelectedItemsChanged();
        }
    }

    /// <summary>
    /// 搜索框文本改变时回调此方法
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnSearch(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            SearchText = e.Value.ToString();
        }
    }

    /// <summary>
    /// 搜索文本框按键回调方法
    /// </summary>
    /// <param name="e"></param>
    protected void OnKeyUp(KeyboardEventArgs e)
    {
        // Escape
        if (e.Key == "Escape")
        {
            ClearSearch();
        }
    }

    /// <summary>
    /// 清空搜索条件方法
    /// </summary>
    protected void ClearSearch()
    {
        SearchText = "";
    }

    private List<SelectedItem> GetShownItems() => (string.IsNullOrEmpty(SearchText)
        ? Items
        : Items.Where(i => i.Text.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList());
}
