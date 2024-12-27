// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class SubMenu
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("has-leaf nav-link")
        .AddClass("active", Item.IsActive)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetIconString => CssBuilder.Default("menu-icon")
        .AddClass(Item.Icon)
        .Build();

    private string? DropdownIconString => CssBuilder.Default("nav-link-right")
        .AddClass(DropdownIcon)
        .Build();

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public MenuItem? Item { get; set; }

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单箭头图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? ArrowIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单项点击回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<MenuItem, Task>? OnClick { get; set; }

    [Microsoft.AspNetCore.Components.CascadingParameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private Menu? Parent { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    /// <summary>
    /// 获得 样式字符串
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetClassString(MenuItem item) => CssBuilder.Default()
        .AddClass("active", !item.IsDisabled && item.IsActive)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView  parameters)
    {
        parameters.SetParameterProperties(this);

        if (Parent == null)
        {
            throw new InvalidOperationException(Localizer["InvalidOperationExceptionMessage"]);
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(Microsoft.AspNetCore.Components.ParameterView .Empty);
    }

    private async Task OnClickItem(MenuItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
