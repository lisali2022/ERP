﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class BootstrapInputGroupIcon
{
    private string? ClassString => CssBuilder.Default("input-group-text")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
#if NET6_0_OR_GREATER
    [Microsoft.AspNetCore.Components.EditorRequired]
#endif
    public string? Icon { get; set; }
}
