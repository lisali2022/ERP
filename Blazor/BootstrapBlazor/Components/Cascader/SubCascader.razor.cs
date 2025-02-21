﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// SubCascader 组件
/// </summary>
public partial class SubCascader
{
    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
#if NET6_0_OR_GREATER
    [Microsoft.AspNetCore.Components.EditorRequired]
#endif
    public IEnumerable<CascaderItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选择项点击回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<CascaderItem, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 子菜单指示图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? SubMenuIcon { get; set; }

    [Microsoft.AspNetCore.Components.CascadingParameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private List<CascaderItem>? SelectedItems { get; set; }

    private string? GetClassString(string classString, CascaderItem item) => CssBuilder.Default(classString)
        .AddClass("active", SelectedItems.Contains(item))
        .Build();

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        Items ??= Enumerable.Empty<CascaderItem>();
    }

    private async Task OnClickItem(CascaderItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
