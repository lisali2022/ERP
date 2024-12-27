// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 组件基类
/// </summary>
public abstract class DialogBase<TModel> : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得/设置 EditModel 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    [System.Diagnostics.CodeAnalysis.NotNull]
    public TModel? Model { get; set; }

    /// <summary>
    /// 获得/设置 BodyTemplate 实例
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
  public Microsoft.AspNetCore.Components.RenderFragment<TModel>? BodyTemplate { get; set; }

    /// <summary>
    /// 获得 数据项集合
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowLabel { get; set; }

    /// <summary>
    /// 获得/设置 每行显示组件数量 默认为 null
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// 获得/设置 设置行格式 默认 Row 布局
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// 获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// 获得/设置 未分组编辑项布局位置 默认 false 在尾部
    /// </summary>
    [Microsoft.AspNetCore.Components.Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Model == null)
        {
            throw new InvalidOperationException("Model value not set to null");
        }
    }
}
