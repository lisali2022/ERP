// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DropdownWidgetItem 组件
/// </summary>
public class DropdownWidgetItem : BootstrapComponentBase
{
    /// <summary>
    /// 获得/设置 挂件图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 DropdownWidgetItem 组件项目的悬浮提示信息
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 徽章颜色 默认为 Color.Success
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color BadgeColor { get; set; } = Color.Success;

    /// <summary>
    /// 获得/设置 Header 颜色 默认为 Color.Primary
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color HeaderColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 徽章显示数量
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? BadgeNumber { get; set; }

    /// <summary>
    /// 获得/设置 是否显示小箭头 默认为 true 显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowArrow { get; set; } = true;

    /// <summary>
    /// 获得/设置 Header 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Body 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Footer 模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 父组件通过级联参数获得
    /// </summary>
    [Microsoft.AspNetCore.Components.CascadingParameter]
    private DropdownWidget? Container { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Container?.Add(this);
    }
}
