﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 确认弹窗按钮组件
/// </summary>
[BootstrapModuleAutoLoader("Button/PopConfirmButton.razor.js")]
public abstract class PopConfirmButtonBase : ButtonBase
{
    /// <summary>
    /// 弹窗位置字符串
    /// </summary>
    protected override string? PlacementString => Placement != Placement.Auto ? Placement.ToDescriptionString() : null;

    /// <summary>
    /// Trigger 字符串
    /// </summary>
    protected override string? TriggerString => Trigger == "click" ? null : Trigger;

    /// <summary>
    /// 获得/设置 是否为 A 标签 默认 false 使用 button 渲染 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsLink { get; set; }

    /// <summary>
    /// 获得/设置 弹窗显示位置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 弹窗触发方式 默认 click
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 自定义内容
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
  public Microsoft.AspNetCore.Components.RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 点击确认时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 点击关闭时回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出 默认 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public Func<Task<bool>>? OnBeforeClick { get; set; }

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 关闭按钮显示文字 默认为 关闭
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮显示文字 默认为 确定
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 确认框图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ConfirmIcon { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmIcon ??= IconTheme.GetIconByKey(ComponentIcons.PopConfirmButtonConfirmIcon);
        Trigger ??= "click";

        OnClose ??= () => Task.CompletedTask;
        OnConfirm ??= () => Task.CompletedTask;
    }
}
