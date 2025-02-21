﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗内关闭按钮组件
/// </summary>
public partial class DialogCloseButton : Button
{
    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public override Color Color { get; set; } = Color.Secondary;

    [Microsoft.AspNetCore.Components.CascadingParameter]
    private Func<Task>? OnCloseAsync { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        Text ??= Localizer[nameof(ModalDialog.CloseButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        await base.HandlerClick();

        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }
    }
}
