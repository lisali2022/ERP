// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ResultDialog 对话框类
/// </summary>
public partial class ResultDialogFooter
{
    /// <summary>
    /// 显示确认按钮
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// 确认按钮文本
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// 确认按钮图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? ButtonYesIcon { get; set; }

    /// <summary>
    /// 确认按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter] public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// 显示取消按钮
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// 取消按钮文本
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// 取消按钮图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ButtonNoIcon { get; set; }

    /// <summary>
    /// 取消按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// 显示关闭按钮
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 关闭按钮文本
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// 关闭按钮图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? ButtonCloseIcon { get; set; }

    /// <summary>
    /// 关闭按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 点击关闭按钮回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnClickClose { get; set; }

    /// <summary>
    /// 获得/设置 点击确认按钮回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnClickYes { get; set; }

    /// <summary>
    /// 获得/设置 点击取消按钮回调方法
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task>? OnClickNo { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<ResultDialogOption>? Localizer { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ButtonCloseText ??= Localizer[nameof(ButtonCloseText)];
        ButtonNoText ??= Localizer[nameof(ButtonNoText)];
        ButtonYesText ??= Localizer[nameof(ButtonYesText)];

        ButtonYesIcon ??= IconTheme.GetIconByKey(ComponentIcons.ResultDialogYesIcon);
        ButtonNoIcon ??= IconTheme.GetIconByKey(ComponentIcons.ResultDialogNoIcon);
        ButtonCloseIcon ??= IconTheme.GetIconByKey(ComponentIcons.ResultDialogCloseIcon);
    }

    private async Task ButtonClick(DialogResult dialogResult)
    {
        if (dialogResult == DialogResult.Yes && OnClickYes != null)
        {
            await OnClickYes();
        }
        if (dialogResult == DialogResult.No && OnClickNo != null)
        {
            await OnClickNo();
        }
        if (dialogResult == DialogResult.Close && OnClickClose != null)
        {
            await OnClickClose();
        }
    }
}
