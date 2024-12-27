// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePicker 组件基类
/// </summary>
public partial class TimePickerBody
{
    /// <summary>
    /// 获得/设置 当前时间
    /// </summary>
    private TimeSpan CurrentTime { get; set; }

    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("time-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Microsoft.AspNetCore.Components.Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Microsoft.AspNetCore.Components.EventCallback<TimeSpan> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮显示文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确定按钮显示文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示秒 默认为 true
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public bool HasSeconds { get; set; } = true;

    private string? HasSecondsCss => HasSeconds ? "has-seconds" : "havenot-seconds";

    /// <summary>
    /// 获得/设置 取消按钮回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<TimeSpan, Task>? OnConfirm { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CurrentTime = Value;
        CancelButtonText ??= Localizer[nameof(CancelButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
    }

    /// <summary>
    /// 点击取消按钮回调此方法
    /// </summary>
    private async Task OnClickClose()
    {
        CurrentTime = Value;
        if (OnClose != null)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// 点击确认按钮时回调此方法
    /// </summary>
    private async Task OnClickConfirm()
    {
        Value = CurrentTime;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnConfirm != null)
        {
            await OnConfirm(Value);
        }
    }
}
