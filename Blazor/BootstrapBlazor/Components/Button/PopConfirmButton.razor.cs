﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// PopConfirmButton 组件
/// </summary>
public partial class PopConfirmButton
{
    private string? ClassString => CssBuilder.Default("pop-confirm")
        .AddClass("disabled", IsDisabled)
        .AddClass(InternalClassName, IsLink)
        .AddClass(ClassName, !IsLink)
        .Build();

    private string? InternalClassName => CssBuilder.Default()
        .AddClass($"link-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string TagName => IsLink ? "a" : "div";

    private string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public override Color Color { get; set; } = Color.None;

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    private bool _renderTooltip;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];

        _renderTooltip = Tooltip == null && !string.IsNullOrEmpty(TooltipText);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override Task ShowTooltip() => Task.CompletedTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override Task RemoveTooltip() => Task.CompletedTask;

    private string? ConfirmString => OnBeforeClick != null ? "true" : null;

    /// <summary>
    /// 显示确认弹窗方法
    /// </summary>
    private async Task Show()
    {
        // 回调消费者逻辑 判断是否需要弹出确认框
        var show = true;
        if (OnBeforeClick != null)
        {
            show = await OnBeforeClick();
        }
        if (show)
        {
            await InvokeVoidAsync("showConfirm", Id);
        }
    }

    /// <summary>
    /// 确认回调方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickConfirm()
    {
        if (IsAsync)
        {
            IsDisabled = true;
            ButtonIcon = LoadingIcon;
            StateHasChanged();
            await Task.Run(() => InvokeAsync(OnConfirm));

            if (ButtonType == ButtonType.Submit)
            {
                await TrySubmit();
            }
            else
            {
                IsDisabled = false;
                ButtonIcon = Icon;
                StateHasChanged();
            }
        }
        else
        {
            await OnConfirm();
            if (ButtonType == ButtonType.Submit)
            {
                await TrySubmit();
            }
        }
    }

    private async Task TrySubmit()
    {
        await InvokeVoidAsync("submit", Id);
    }
}
