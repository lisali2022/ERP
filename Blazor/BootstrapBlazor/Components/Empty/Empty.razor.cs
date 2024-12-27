// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Empty
{
    private string? ClassString => CssBuilder.Default("empty")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  获得/设置 图片路径 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Image { get; set; }

    /// <summary>
    /// 获得/设置 空状态描述 默认为 无数据
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 自定义模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? Template { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Empty>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
    }
}
