﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoRedirect component
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "autoredirect", JSObjectReference = true)]
public class AutoRedirect : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得/设置 重定向地址
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// 获得/设置 是否强制导航 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsForceLoad { get; set; }

    /// <summary>
    /// 获得/设置 自动锁屏间隔单位 秒 默认 60000 毫秒
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int Interval { get; set; } = 60000;

    /// <summary>
    /// 获得/设置 地址跳转前回调方法 返回 true 时中止跳转
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Func<Task<bool>>? OnBeforeRedirectAsync { get; set; }

    /// <summary>
    /// 获得/设置 NavigationManager 实例
    /// </summary>
     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private Microsoft.AspNetCore.Components.NavigationManager? NavigationManager { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Interval, nameof(Lock));

    /// <summary>
    /// 锁屏操作由 JS 调用
    /// </summary>
    [Microsoft.JSInterop.JSInvokable]
    public async Task Lock()
    {
        var interrupt = false;
        if (OnBeforeRedirectAsync != null)
        {
            interrupt = await OnBeforeRedirectAsync();
        }
        if (!interrupt && !string.IsNullOrEmpty(RedirectUrl))
        {
            NavigationManager.NavigateTo(RedirectUrl, IsForceLoad);
        }
    }
}
