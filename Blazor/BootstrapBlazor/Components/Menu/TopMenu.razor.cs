// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 顶栏菜单
/// </summary>
public partial class TopMenu
{
    private string? GetDropdownClassString(MenuItem item, string className = "") => CssBuilder.Default(className)
        .AddClass("dropdown", string.IsNullOrEmpty(className) && !Parent.IsBottom)
        .AddClass("dropup", string.IsNullOrEmpty(className) && Parent.IsBottom)
        .AddClass("disabled", item.IsDisabled)
        .AddClass("active", item.IsActive)
        .Build();

    private static string? GetIconString(string icon) => CssBuilder.Default("menu-icon")
        .AddClass(icon)
        .Build();

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单箭头图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ArrowIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单数据集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public IEnumerable<MenuItem>? Items { get; set; }

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

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.TopMenuDropdownIcon);
        ArrowIcon ??= IconTheme.GetIconByKey(ComponentIcons.MenuLinkArrowIcon);
    }

    private async Task OnClickItem(MenuItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
