﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ListView 组件基类
/// </summary>
public partial class Logout
{
    private string? LogoutClassString => CssBuilder.Default("dropdown dropdown-logout")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件当前用户头像
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户显示名称
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户显示名称前置文本 默认 欢迎
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? PrefixDisplayNameText { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户登录账号
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? UserName { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示用户名 默认 true 显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowUserName { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件当前用户登录账号前置文本 默认 当前账号
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? PrefixUserNameText { get; set; }

    /// <summary>
    /// 获得/设置 组件 HeaderTemplate
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件 LinkTemplate
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? LinkTemplate { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IStringLocalizer<Logout>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        PrefixDisplayNameText ??= Localizer[nameof(PrefixDisplayNameText)];
        PrefixUserNameText ??= Localizer[nameof(PrefixUserNameText)];
    }
}
