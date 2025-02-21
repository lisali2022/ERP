﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax 组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "ajax", AutoInvokeInit = false, AutoInvokeDispose = false)]
public class Ajax : BootstrapModuleComponentBase
{
     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private AjaxService? AjaxService { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        AjaxService.Register(this, InvokeAsync);
        AjaxService.RegisterGoto(this, Goto);
    }

    private Task<JsonDocument?> InvokeAsync(AjaxOption option) => InvokeAsync<JsonDocument?>("execute", option);

    private Task Goto(string url) => InvokeVoidAsync("goto", url);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            AjaxService.UnRegister(this);
            AjaxService.UnRegisterGoto(this);
        }

        await base.DisposeAsync(disposing);
    }
}
