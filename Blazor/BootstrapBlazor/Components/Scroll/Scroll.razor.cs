// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// Scroll 组件
/// </summary>
public partial class Scroll
{
    private string? ClassString => CssBuilder.Default("scroll")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height))
        .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
        .AddClass($"--bb-scroll-width: {ActualScrollWidth}px;")
        .AddClass($"--bb-scroll-hover-width: {ActualScrollHoverWidth}px;")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private int ActualScrollWidth => ScrollWidth ?? Options.CurrentValue.ScrollOptions.ScrollWidth;

    private int ActualScrollHoverWidth => ScrollHoverWidth ?? Options.CurrentValue.ScrollOptions.ScrollHoverWidth;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 组件高度
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// 获得/设置 组件宽度
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 滚动条宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollWidth"/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int? ScrollWidth { get; set; }

    /// <summary>
    /// 获得/设置 滚动条 hover 状态下宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollHoverWidth"/>
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int? ScrollHoverWidth { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }
}
