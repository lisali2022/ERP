﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Search 组件
/// </summary>
public partial class Search
{
    [System.Diagnostics.CodeAnalysis.NotNull]
    private string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示清除按钮 默认为 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// Clear button icon
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? ClearButtonIcon { get; set; }

    /// <summary>
    /// Clear button text
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// Clear button color
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color ClearButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 搜索按钮颜色
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color SearchButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 搜索按钮图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? SearchButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 正在搜索按钮图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? SearchButtonLoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 点击搜索后是否自动清空搜索框
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsAutoClearAfterSearch { get; set; }

    /// <summary>
    /// 获得/设置 搜索模式是否为输入即触发 默认 false 点击搜索按钮触发
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsOnInputTrigger { get; set; }

    /// <summary>
    /// 获得/设置 搜索按钮文字
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public string? SearchButtonText { get; set; }

    /// <summary>
    /// 获得/设置 点击搜索按钮时回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<string, Task>? OnSearch { get; set; }

    /// <summary>
    /// 获得/设置 点击清空按钮时回调委托
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<string, Task>? OnClear { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Search>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? ClassString => CssBuilder.Default("search")
        .AddClassFromAttributes(AdditionalAttributes)
        .AddClass(base.ClassString)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SearchButtonText ??= Localizer[nameof(SearchButtonText)];

        SkipEnter = true;
        SkipEsc = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchClearButtonIcon);
        SearchButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonIcon);
        SearchButtonLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);

        ButtonIcon = SearchButtonIcon;
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnSearchClick()
    {
        if (OnSearch != null)
        {
            ButtonIcon = SearchButtonLoadingIcon;
            await OnSearch(CurrentValueAsString);
            ButtonIcon = SearchButtonIcon;
        }

        if (IsAutoClearAfterSearch)
        {
            CurrentValueAsString = "";
        }

        await FocusAsync();
    }

    /// <summary>
    /// 点击搜索按钮时触发此方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClearClick()
    {
        if (OnClear != null)
        {
            await OnClear(CurrentValueAsString);
        }
        CurrentValueAsString = "";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected override async Task CustomKeyUp(string key)
    {
        if (!string.IsNullOrEmpty(CurrentValueAsString))
        {
            if (key == "Escape")
            {
                if (OnEscAsync != null)
                {
                    await OnEscAsync(Value);
                }

                // 清空
                await OnClearClick();
            }

            if (IsOnInputTrigger || key == "Enter")
            {
                if (OnEnterAsync != null)
                {
                    await OnEnterAsync(Value);
                }

                // 搜索
                await OnSearchClick();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override async Task OnClickItem(string item)
    {
        await base.OnClickItem(item);

        if (IsOnInputTrigger)
        {
            await OnSearchClick();
        }
    }
}
