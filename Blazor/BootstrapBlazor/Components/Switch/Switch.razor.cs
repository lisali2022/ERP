﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Switch
{
    private string? ClassName => CssBuilder.Default("switch")
        .AddClass("is-checked", Value)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? CoreClassName => CssBuilder.Default("switch-core")
        .AddClass($"border-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"bg-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"border-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .AddClass($"bg-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .Build();

    private string? GetInnerText()
    {
        string? ret = null;
        if (ShowInnerText)
        {
            ret = Value ? OnInnerText : OffInnerText;
        }

        return ret;
    }

    /// <summary>
    /// 获得 显示文字
    /// </summary>
    private string? Text => Value ? OnText : OffText;

    /// <summary>
    /// 获得 组件最小宽度
    /// </summary>
    private string? SwitchStyleName => CssBuilder.Default()
        .AddClass($"min-width: {Width}px;", Width > 0)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 Style 集合
    /// </summary>
    protected override string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height >= 20)
        .Build();

    /// <summary>
    /// 获得/设置 开颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color OnColor { get; set; } = Color.Success;

    /// <summary>
    /// 获得/设置 关颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color OffColor { get; set; }

    /// <summary>
    /// 获得/设置 组件宽度 默认 40
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public override int Width { get; set; } = 40;

    /// <summary>
    /// 获得/设置 控件高度默认 20px
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Height { get; set; } = 20;

    /// <summary>
    /// 获得/设置 组件 On 时内置显示文本
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? OnInnerText { get; set; }

    /// <summary>
    /// 获得/设置 组件 Off 时内置显示文本
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? OffInnerText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示内置文字 默认 false 显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowInnerText { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Switch>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnInnerText ??= Localizer[nameof(OnInnerText)];
        OffInnerText ??= Localizer[nameof(OffInnerText)];
    }

    /// <summary>
    /// 点击控件时触发此方法
    /// </summary>
    private async Task OnClick()
    {
        if (!IsDisabled)
        {
            Value = !Value;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            // 回调 OnValueChanged 再调用 Microsoft.AspNetCore.Components.EventCallback
            if (OnValueChanged != null)
            {
                await OnValueChanged(Value);
            }
        }
    }
}
