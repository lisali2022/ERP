﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TooltipWrapperBase 基类
/// </summary>
public abstract class TooltipWrapperBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// Tooltip 弹窗位置字符串
    /// </summary>
    protected virtual string? PlacementString => (!string.IsNullOrEmpty(TooltipText) && TooltipPlacement != Placement.Auto) ? TooltipPlacement.ToDescriptionString() : null;

    /// <summary>
    /// Tooltip Trigger 字符串
    /// </summary>
    protected virtual string? TriggerString => TooltipTrigger == "hover focus" ? null : TooltipTrigger;

    /// <summary>
    /// the instance of Tooltip component
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    protected Tooltip? Tooltip { get; set; }

    /// <summary>
    /// 获得/设置 TooltipText 显示文字 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// 获得/设置 Tooltip 显示位置 默认为 Top
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Placement TooltipPlacement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 Tooltip 触发方式 默认为 hover focus
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? TooltipTrigger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipTrigger ??= "hover focus";
    }
}
