﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Anchor 组件部分类
/// </summary>
public partial class Anchor
{
    /// <summary>
    /// 获得/设置 目标组件 Id
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 滚动组件 Id 默认为 null 使用最近滚动条容器元素
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Container { get; set; }

    /// <summary>
    /// 获得/设置 滚动时是否开启动画 默认 true
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsAnimation { get; set; } = true;

    /// <summary>
    /// 获得 滚动动画
    /// </summary>
    protected string? AnimationString => IsAnimation ? "true" : null;

    /// <summary>
    /// 获得/设置 距离顶端偏移量 默认为 0
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Offset { get; set; }

    /// <summary>
    /// 获得/设置 子内容
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    private string? GetTargetString => string.IsNullOrEmpty(Target) ? null : Target;

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
