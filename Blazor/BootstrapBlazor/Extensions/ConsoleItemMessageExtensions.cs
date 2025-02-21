﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components;

static class ConsoleMessageItemExtensions
{
    /// <summary>
    /// 渲染消息方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static Microsoft.AspNetCore.Components.RenderFragment RenderMessage(this ConsoleMessageItem item) => builder =>
    {
        if (item.IsHtml)
        {
            builder.AddContent(0, new Microsoft.AspNetCore.Components.MarkupString(item.Message));
        }
        else
        {
            builder.AddContent(0, item.Message);
        }
    };
}
