﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// 客户端链接组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "hub", JSObjectReference = true)]
public class ConnectionHub : BootstrapModuleComponentBase
{
     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IConnectionService? ConnectionService { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private Microsoft.AspNetCore.Components.NavigationManager? NavigationManager { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IIpLocatorFactory? IpLocatorFactory { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IThrottleDispatcherFactory? ThrottleDispatcherFactory { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    private IIpLocatorProvider? _ipLocatorProvider;

    private ThrottleOptions? _throttleOptions;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var options = BootstrapBlazorOptions.Value.ConnectionHubOptions;
        if (options.Enable)
        {
            _throttleOptions = new ThrottleOptions() { Interval = options.BeatInterval };
            await InvokeVoidAsync("init", Id, new
            {
                Invoke = Interop,
                Method = nameof(Callback),
                ConnectionId = Guid.NewGuid(),
                Interval = options.BeatInterval.TotalMilliseconds,
                Url = "ip.axd"
            });
        }
    }

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [Microsoft.JSInterop.JSInvokable]
    public async Task Callback(ClientInfo client)
    {
        var code = client.Id;
        if (!string.IsNullOrEmpty(code))
        {
            var dispatch = ThrottleDispatcherFactory.GetOrCreate(code, _throttleOptions);
            await dispatch.ThrottleAsync(async () =>
            {
                client.RequestUrl = NavigationManager.Uri;

                if (BootstrapBlazorOptions.Value.ConnectionHubOptions.EnableIpLocator)
                {
                    _ipLocatorProvider ??= IpLocatorFactory.Create(BootstrapBlazorOptions.Value.IpLocatorOptions.ProviderName);
                    client.City = await _ipLocatorProvider.Locate(client.Ip);
                }
                ConnectionService.AddOrUpdate(client);
            });
        }
    }
}
