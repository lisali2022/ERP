﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Rate 组件
/// </summary>
public partial class Rate
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("rate")
        .AddClass("text-nowrap", !IsWrap)
        .AddClass("disabled", IsDisable)
        .AddClass("readonly", IsReadonly)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(int i) => CssBuilder.Default("rate-item")
        .AddClass("is-on", Value >= i)
        .Build();

    /// <summary>
    /// 判断是否显示部分星级
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private bool IsPartialStar(int i) => (Value + 1 - i) is > 0 and < 1;

    private string GetIcon(int i) => Value >= i ? StarIcon : UnStarIcon;

    private string GetWidthStyle(int i) => $"width: {Math.Round(Value + 1 - i, 2) * 100}%;";

    /// <summary>
    /// 获得/设置 选中图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? StarIcon { get; set; }

    /// <summary>
    /// 获得/设置 未选中图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? UnStarIcon { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public double Value { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    /// <remarks>禁用模式下图标颜色为灰色，不可点击</remarks>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsDisable { get; set; }

    /// <summary>
    /// 获得/设置 是否只读 默认为 false
    /// </summary>
    /// <remarks>只读情况下图标为彩色，仅不可点击</remarks>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsReadonly { get; set; }

    /// <summary>
    /// 获得/设置 是否禁止换行 默认为 true
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Value 默认为 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowValue { get; set; }

    /// <summary>
    /// 获得/设置 子项模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<double>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<double> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<double, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 最大值 默认 5
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Max { get; set; } = 5;

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        StarIcon ??= IconTheme.GetIconByKey(ComponentIcons.RateStarIcon);
        UnStarIcon ??= IconTheme.GetIconByKey(ComponentIcons.RateUnStarIcon);

        if (Max < 1)
        {
            Max = 5;
        }

        if (Value < 0)
        {
            Value = 0;
        }
    }

    private async Task OnClickItem(int value)
    {
        Value = value;
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        else
        {
            StateHasChanged();
        }
    }
}
