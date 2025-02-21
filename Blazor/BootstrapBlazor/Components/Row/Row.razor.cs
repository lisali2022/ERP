﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Row
{
    /// <summary>
    /// 获得/设置 设置一行显示多少个子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public ItemsPerRow ItemsPerRow { get; set; }

    /// <summary>
    /// 获得/设置 设置行格式 默认 Row 布局
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// 获得/设置 子 Row 跨父 Row 列数 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int? ColSpan { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }
}
