// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectBase 组件基类
/// </summary>
public abstract class SelectBase<TValue> : PopoverSelectBase<TValue>
{
    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 设置搜索图标
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否为 Microsoft.AspNetCore.Components.MarkupString( 默认 false
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// 获得/设置 字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 选项模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 分组项模板
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<string>? GroupItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 IIconTheme 服务实例
    /// </summary>
     [Microsoft.AspNetCore.Components.Inject]
    [System.Diagnostics.CodeAnalysis.NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得/设置 搜索框文本
    /// </summary>
    [System.Diagnostics.CodeAnalysis.NotNull]
    protected string? SearchText { get; set; }

    /// <summary>
    /// 获得 SearchIcon 图标字符串 默认增加 icon 样式
    /// </summary>
    protected string? SearchIconString => CssBuilder.Default("icon")
        .AddClass(SearchIcon)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? CustomClassString => CssBuilder.Default()
        .AddClass("select", IsPopover)
        .AddClass(base.CustomClassString)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectSearchIcon);
    }

    /// <summary>
    /// 显示下拉框方法
    /// </summary>
    /// <returns></returns>
    public Task Show() => InvokeVoidAsync("show", Id);

    /// <summary>
    /// 关闭下拉框方法
    /// </summary>
    /// <returns></returns>
    public Task Hide() => InvokeVoidAsync("hide", Id);
}
