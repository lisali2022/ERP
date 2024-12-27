// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Alert 警告框组件
/// </summary>
public abstract class AlertBase : BootstrapComponentBase
{
    /// <summary>
    /// 获得 图标样式字符串
    /// </summary>
    protected string? IconString => CssBuilder.Default("alert-icon")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// 获得/设置 颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示关闭按钮
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowDismiss { get; set; }

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示左侧 Bar
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowBar { get; set; }

    /// <summary>
    /// 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 关闭警告框回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnDismiss { get; set; }
}
