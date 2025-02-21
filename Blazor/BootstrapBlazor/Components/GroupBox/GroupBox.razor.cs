﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class GroupBox
{
    private string? ClassString => CssBuilder.Default("groupbox")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 Title 属性 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Title { get; set; }
}
