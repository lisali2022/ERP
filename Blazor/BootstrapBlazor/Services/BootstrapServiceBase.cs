﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class BootstrapServiceBase<TOption>
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    protected List<(Microsoft.AspNetCore.Components.ComponentBase Key, Func<TOption, Task> Callback)> Cache { get; } = [];

    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    protected async Task Invoke(TOption option, ComponentBase? component = null)
    {
        var (Key, Callback) = component != null
            ? Cache.FirstOrDefault(k => k.Key == component)
            : Cache.FirstOrDefault();
        if (Callback == null)
        {
#if NET8_0_OR_GREATER
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-webapp step 7 for BootstrapBlazorRoot");
#else
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-server step 7 for BootstrapBlazorRoot");
#endif
        }
        await Callback.Invoke(option);
    }

    /// <summary>
    /// 注册弹窗事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(Microsoft.AspNetCore.Components.ComponentBase key, Func<TOption, Task> callback)
    {
        Cache.Add((key, callback));
    }

    /// <summary>
    /// 注销弹窗事件
    /// </summary>
    internal void UnRegister(Microsoft.AspNetCore.Components.ComponentBase key)
    {
        var item = Cache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null) Cache.Remove(item);
    }
}
