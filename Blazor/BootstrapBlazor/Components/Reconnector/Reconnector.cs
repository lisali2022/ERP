// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Reconnector 组件
/// </summary>
public class Reconnector : Microsoft.AspNetCore.Components.ComponentBase, IReconnector
{
    /// <summary>
    /// 获得/设置 正在尝试重试连接对话框的模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ReconnectingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 连接失败对话框的模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ReconnectFailedTemplate { get; set; }

    /// <summary>
    /// 获得/设置 连接被拒绝对话框的模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? ReconnectRejectedTemplate { get; set; }

     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    private IReconnectorProvider? Provider { get; set; }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        Provider.NotifyContentChanged(this);
    }
}
